using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRTech.Application.Common;
using HRTech.Application.Models;
using HRTech.Application.Services.User.Interfaces;
using HRTech.Domain;
using HRTech.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HRTech.WebApi.Controllers.User
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            IUserService userService, 
            IConfiguration configuration, 
            IMapper mapper, 
            UserManager<ApplicationUser> userManager): base(mapper, userManager)
        {
            _userService = userService;
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetUserByEmail(loginRequest.Email);
            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userService.CheckPasswordSignIn(user, loginRequest.Password);
            if (result)
            {
                return await GetToken(user);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(loginRequest);
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOut();
            return Ok("Logged out");
        }

        [HttpPost("addUserInCompany")]
        public async Task<IActionResult> AddUserInCompany([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<ApplicationUser>(registerRequest);
            var result = await _userService.CreateUser(user);
            if (result.Succeeded)
            {
                return Ok(user);
            }
            AddErrors(result);
            return BadRequest(result);
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<ApplicationUser>(registerRequest);
            var result = await _userService.Create(user, registerRequest.Password);
            if (result.Succeeded)
            {
                return Ok(user);
            }

            AddErrors(result);
            return BadRequest(result);
        }
        
        [HttpGet("info")]
        public async Task<IActionResult> GetUserInfo()
        {
            return Ok(_mapper.Map<UserDto>(await GetCurrentUser()));
        }
        
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete()
        {
            IdentityResult result = await _userManager.DeleteAsync(await GetCurrentUser());
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }

        [AllowAnonymous]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userService.GetUserById(userId);
            return Ok(_mapper.Map<UserDto>(user));
        }
        
        [HttpPut("UpdateGrade/{gradeId}")]
        public async Task<IActionResult> UpdateGrade(int gradeId)
        {
            var result = _userService.UpdateGrade(await GetCurrentUser(), gradeId, CancellationToken.None);
            return Ok(result);
        }

        [HttpGet("IsAdmin")]
        public async Task<IActionResult> IsAdmin()
        {
            var user = await GetCurrentUser();
            var result = await _userService.IsInRole(user.Id, RolesConst.Admin);
            return Ok(result);
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private async Task<IActionResult> GetToken(ApplicationUser user)
        {
            var claims = await _userService.GetValidClaims(user);
            var token = new JwtSecurityToken
            (
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["Token:Key"])), 
                    SecurityAlgorithms.HmacSha256)
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}