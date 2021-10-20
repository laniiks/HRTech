using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
        Task<List<Claim>> GetValidClaims(ApplicationUser user);
    }
}