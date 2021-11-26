using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Enums;
using HRTech.Application.Services.User.Interfaces;
using HRTech.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Controllers.User
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager, IMapper mapper) : base(mapper, userManager)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("GetAllExpertUserInCompany")]
        public async Task<IActionResult> GetAllExpertUserInCompany(Guid companyId, ExpertUserState expertUserState,
            CancellationToken cancellationToken)
        {
            var user = await GetCurrentUser();
            var result = await _userService.GetAllExpertUserInCompany(user.Id, companyId, expertUserState, cancellationToken);
            return Ok(result);
        }
        [HttpGet("GetAllUserInCompany")]
        public async Task<IActionResult> GetAllExpertUserInCompany(Guid companyId,
            CancellationToken cancellationToken)
        {
            var result = await _userService.GetAllUserInCompany(companyId, cancellationToken);
            return Ok(result);
        }

        [HttpGet("IsDirector")]
        public async Task<IActionResult> IsDirector(Guid companyId, CancellationToken cancellationToken)
        {
            var result = await _userService.IsDirector(await GetCurrentUser(), companyId, cancellationToken);
            return Ok(result);
        }

        [HttpPost("AddPhotoUser")]
        public async Task<IActionResult> AddPhotoUser(IFormFile photo, CancellationToken cancellationToken)
        {
            var file = GetFileInfo(photo);
            var result = await _userService.AddPhotoUser(await GetCurrentUser(), file, cancellationToken);
            return Ok(result);
        }
    }
}