using System;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Services.Grade.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Controllers.Grade
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : BaseController
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet("GetByIdGrade/{id}")]
        public async Task<IActionResult> GetByIdGrade(int id, CancellationToken cancellationToken)
        {
            var result = await _gradeService.GetByIdGrade(id, cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetAllGradesInCompany/{companyId}")]
        public async Task<IActionResult> GetAllGradesInCompany(Guid companyId, CancellationToken cancellationToken)
        {
            var result = await _gradeService.GetAllGradeInCompany(companyId, cancellationToken);
            return Ok(result);
        }

        
    }
}