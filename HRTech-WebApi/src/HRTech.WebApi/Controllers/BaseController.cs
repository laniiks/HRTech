using System;
using System.Collections.Generic;
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
        protected string GetContentType(string fileName)  
        {  
            var types = GetMimeTypes();  
            var ext = Path.GetExtension(fileName).ToLowerInvariant();  
            return types[ext];  
        }  
   
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "Application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}