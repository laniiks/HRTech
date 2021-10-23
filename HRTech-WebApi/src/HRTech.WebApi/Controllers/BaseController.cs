using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using HRTech.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public BaseController(
            IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public BaseController() { }

        protected async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}