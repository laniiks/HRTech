using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Common.Enums;
using HRTech.Application.Models;
using HRTech.Domain;
using Microsoft.AspNetCore.Identity;

namespace HRTech.Application.Services.User.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<ApplicationUser> GetUserById(string userId);
        Task<bool> CheckPasswordSignIn(ApplicationUser user, string password);
        Task SignOut();
        Task<bool> IsInRole(string userId, string role);
        Task<IdentityResult> Create(ApplicationUser user, string password);
        Task<ICollection<IdentityResult>> CreateRange(List<RegisterDto> users);

        Task<List<Claim>> GetValidClaims(ApplicationUser user);
        Task<int> UpdateGrade(ApplicationUser user, int idGrade, CancellationToken cancellationToken);

        Task<ICollection<ApplicationUser>> GetAllExpertUserInCompany(string userId, Guid companyId, ExpertUserState expertUserState,
            CancellationToken cancellationToken);

        Task<bool> IsDirector(ApplicationUser applicationUser, Guid companyId, CancellationToken cancellationToken);
        Task<string> AddPhotoUser(ApplicationUser user, FileDto fileDto, CancellationToken cancellationToken);
        Task<ICollection<ApplicationUser>> GetAllUserInCompany(Guid companyId, CancellationToken cancellationToken);
    }
}