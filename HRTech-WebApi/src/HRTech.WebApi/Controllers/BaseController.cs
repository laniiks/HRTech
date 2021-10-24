using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using HRTech.Application.Models;
using HRTech.Domain;
using Microsoft.AspNetCore.Http;
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

        protected FileDto GetFileInfo(IFormFile file)
        {
            if (file is {Length: > 0})
            {
                var fileName = file.FileName;
                var fileExtension = Path.GetExtension(fileName);
                using var target = new MemoryStream();
                target.Position = 0;
                file.CopyToAsync(target);
                return new FileDto
                {
                    FileGuid = Guid.NewGuid(),
                    FileName = fileName,
                    FileType = fileExtension,
                    Content = target.ToArray()
                };
            }

            return null;
        }
    }
}