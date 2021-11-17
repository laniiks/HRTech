using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Services.TemplateFile.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : BaseController
    {
        private readonly ITemplateFileService _templateFileService;

        public UploadFileController(ITemplateFileService templateFileService)
        {
            _templateFileService = templateFileService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            var result = GetFileInfo(Request.Form.Files[0]);
            return Ok(result);
        }

        [HttpPost("UploadTemplateFile")]
        public async Task<IActionResult> UploadTemplateFile(CancellationToken cancellationToken)
        {
            var fileInfo = GetFileInfo(Request.Form.Files[0]);
            var result = await _templateFileService.CreateTemplateFile(fileInfo, cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetLastTemplateFile")]
        public async Task<IActionResult> GetLastTemplateFile(CancellationToken cancellationToken)
        {
            var result = await _templateFileService.GetLastTemplateFile(cancellationToken);
            return Ok(result);
        }

        [HttpGet("DownloadTemplateFile/{id}")]
        public async Task<IActionResult> DownloadTemplateFile(int id, CancellationToken cancellationToken)
        {
            var file = await _templateFileService.DownloadTemplateFile(id, cancellationToken);
            return File(file.Content, GetContentType(file.FileName), file.FileName);
        }
            
         
    }
}