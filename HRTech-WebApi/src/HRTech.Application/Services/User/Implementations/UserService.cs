using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Enums;
using Common.HtmlMessage;
using HRTech.Application.Abstractions;
using HRTech.Application.Common;
using HRTech.Application.Models;
using HRTech.Application.Services.User.Contracts;
using HRTech.Application.Services.User.Interfaces;
using HRTech.Domain;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HRTech.Application.Services.User.Implementations
{
    public class UserService: IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly HtmlMessage _htmlMessage;
        private readonly IGradeRepository _gradeRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;


        public UserService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager, 
            ILogger<UserService> logger, IMapper mapper, ISendEndpointProvider sendEndpointProvider, HtmlMessage htmlMessage, IGradeRepository gradeRepository, IApplicationUserRepository applicationUserRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
            _sendEndpointProvider = sendEndpointProvider;
            _htmlMessage = htmlMessage;
            _gradeRepository = gradeRepository;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> CheckPasswordSignIn(ApplicationUser user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
            return result.Succeeded;          
        }

        public Task SignOut()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<bool> IsInRole(string userId, string role)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            
            if (identityUser == null)
            {
                _logger.LogError("Пользователь с Id {userId} не найден", userId);
                throw new Exception("Пользователь не найден");
            }

            return await _userManager.IsInRoleAsync(identityUser, role);            
        }

        public async Task<IdentityResult> Create(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return result;
            }

            await _userManager.AddToRoleAsync(user, RolesConst.User);
            await _signInManager.SignInAsync(user, isPersistent: false );
            
            var name = user.FirstName + " " + user.LastName + " " + user.Patronymic;
            var message = _htmlMessage.RegistrationConfirmation(name, user.Email, password);
            message.ToMessageBody();
            var sendNotification = new SendNotification
            {
                Recipient = user.Email,
                Subject = "Учетные данные",
                Message = message.HtmlBody
            };

            var endPoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:send_email"));
            await endPoint.Send<SendNotification>(sendNotification);
            
            _logger.LogInformation("Создан новый пользователь {@UserStruct}", new
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
            return result;
            
        }

        public async Task<ICollection<IdentityResult>> CreateRange(List<RegisterDto> users)
        {
            ICollection<IdentityResult> result = null;
            foreach (var value in users)
            {
                var dto = new ApplicationUser
                {
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    Patronymic = value.Patronymic,
                    Email = value.Email,
                    //Password = value.Password,
                    PhoneNumber = value.PhoneNumber,
                    CompanyId = value.CompanyId,
                    UserName = value.Email,
                    ExpertUserState = (ExpertUserState)Int16.Parse(value.ExpertUserState)
                };
                result = new[] {await Create(dto, value.Password)};
            }

            return result;
        }

        public async Task<List<Claim>> GetValidClaims(ApplicationUser user)
        {
            var options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id),
                new Claim(options.ClaimsIdentity.UserNameClaimType, user.UserName)
            };
            
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userClaims);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByIdAsync(userRole);
                if (role == null)
                {
                    continue;
                }

                var roleClaims = await _roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }

            return claims;        
        }

        public async Task<int> UpdateGrade(ApplicationUser user, int idGrade, CancellationToken cancellationToken)
        {
            var users = await _userManager.FindByIdAsync(user.Id);
            users.GradeId = idGrade;
            await _userManager.UpdateAsync(users);
            return idGrade;
        }

        public async Task<ICollection<ApplicationUser>> GetAllExpertUserInCompany(string userId, Guid companyId,
            ExpertUserState expertUserState, CancellationToken cancellationToken)
        {
            var us = await _applicationUserRepository.GetAllExpertUserInCompany(userId, companyId, expertUserState,
                cancellationToken);
            var users = _mapper.Map<ICollection<UserDto>>(us);
            if (users == null)
            {
                throw new Exception("Не найдено");
            }

            return _mapper.Map<ICollection<ApplicationUser>>(users);
        }
    }
}