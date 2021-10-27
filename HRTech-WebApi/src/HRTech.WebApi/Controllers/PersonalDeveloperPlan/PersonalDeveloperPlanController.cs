using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRTech.Application.Models;
using HRTech.Application.Services.PDP.Interfaces;
using HRTech.Domain;
using HRTech.WebApi.Models.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Controllers.PersonalDeveloperPlan
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalDeveloperPlanController : BaseController
    {
        private readonly IPersonalDeveloperPlanService _personalDeveloperPlanService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public PersonalDeveloperPlanController(
            UserManager<ApplicationUser> userManager, 
            IMapper mapper, 
            IPersonalDeveloperPlanService personalDeveloperPlanService): base(mapper, userManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _personalDeveloperPlanService = personalDeveloperPlanService;
        }
        
        [HttpPost("AddPdpForUser")]
        public async Task<IActionResult> AddPdpForUser(string title, IFormFile formFile,
            CancellationToken cancellationToken)
        {
            var file = GetFileInfo(formFile);
            var dto = new PersonalDevelopmentPlanDto
            {
                Title = title,
                FileGuid = file.FileGuid,
                FileName = file.FileName,
                FileType = file.FileType,
                Content = file.Content,
                CreatedDateTime = DateTime.UtcNow
            };

            var result = await _personalDeveloperPlanService.AddPdpdForUser(await GetCurrentUser(), dto, cancellationToken);
            return Ok(result);
        }
        
        [HttpPost("GetAllPdpForUser")]
        public async Task<IActionResult> GetAllPdpForUser(CancellationToken cancellationToken)
        {
            var result = await _personalDeveloperPlanService.GetAllPdpForUser(await GetCurrentUser(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("DownloadPersonalDeveloperPlan/{fileGuid}")]
        public async Task<IActionResult> DownloadPdpFile(Guid fileGuid)
        {
            var fileDto = await _personalDeveloperPlanService.GetFileAsync(fileGuid);

            return File(fileDto.Content, "application/octet-stream", fileDto.FileName);
        }

    }
}