using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Enums;
using HRTech.Application.Models;
using HRTech.Application.Services.Evaluation.Interfaces;
using HRTech.Domain;
using HRTech.WebApi.Models.Evaluation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Controllers.Evaluation
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : BaseController
    {
        private readonly IEvaluationService _evaluationService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;


        public EvaluationController(IEvaluationService evaluationService, IMapper mapper, 
            UserManager<ApplicationUser> userManager):base(mapper, userManager)
        {
            _evaluationService = evaluationService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("CreateEvaluation")]
        public async Task<IActionResult> CreateEvaluation([FromBody]CreateEvaluationRequest createEvaluationRequest, CancellationToken cancellationToken)
        {
            var evaluation = new EvaluationDto
            {
                DateOfEvaluation = createEvaluationRequest.DateOfEvaluation,
                ApplicationUserIdExpertSoftSkills = createEvaluationRequest.ApplicationUserIdExpertSoftSkills,
                ApplicationUserIdExpertHardSkills = createEvaluationRequest.ApplicationUserIdExpertHardSkills,
                ApplicationUserIdExpertEnglishSkills = createEvaluationRequest.ApplicationUserIdExpertEnglishSkills,
                EvaluationState = EvaluationState.New,
                CreatedDateTime = DateTime.UtcNow
            };
            var result = await _evaluationService.CreateEvaluation(evaluation, await GetCurrentUser(),
                cancellationToken);
            return Ok(result);
        }
        [HttpGet("GetByIdEvaluation/{evaluationId}")]
        public async Task<IActionResult> GetByIdEvaluation(Guid evaluationId, CancellationToken cancellationToken)
        {
            var result = await _evaluationService.GetByIdEvaluation(evaluationId, cancellationToken);
            return Ok(result);
        }
        
        [HttpGet("GetAllEvaluationForUser")]
        public async Task<IActionResult> GetAllEvaluationForUser(CancellationToken cancellationToken)
        {
            var result = await _evaluationService.GetAllResponseEvaluationForUser(await GetCurrentUser(), cancellationToken);
            return Ok(result);
        }
        
        [HttpGet("GetAllResponseEvaluationForExpertUser")]
        public async Task<IActionResult> GetAllResponseEvaluationForExpertUser(CancellationToken cancellationToken)
        {
            var result = await _evaluationService.GetAllResponseEvaluationForExpertUser(await GetCurrentUser(), cancellationToken);
            return Ok(result);
        }
        
        [HttpGet("GetAllResponseEvaluationInCompany/{companyId}")]
        public async Task<IActionResult> GetAllResponseEvaluationInCompany(Guid companyId, CancellationToken cancellationToken)
        {
            var result = await _evaluationService.GetAllResponseEvaluationInCompany(companyId, cancellationToken);
            return Ok(result);
        }
        
        [HttpPut("SuccessEvaluationSoftSkill")]
        public async Task<IActionResult> SuccessEvaluationSoftSkill(Guid evaluationId, EvaluationSuccessState skillSuccess,
            CancellationToken cancellationToken)
        {
            var result = await _evaluationService.SoftSkillSuccess(evaluationId, skillSuccess, await GetCurrentUser(), cancellationToken);
            return Ok(result);
        }
        
        [HttpPut("SuccessEvaluationHardSkill")]
        public async Task<IActionResult> SuccessEvaluationHardSkill(Guid evaluationId, EvaluationSuccessState skillSuccess,
            CancellationToken cancellationToken)
        {
            var result = await _evaluationService.HardSkillSuccess(evaluationId, skillSuccess, await GetCurrentUser(), cancellationToken);
            return Ok(result);
        }
        
        [HttpPut("SuccessEvaluationEnglishSkill")]
        public async Task<IActionResult> SuccessEvaluationEnglishSkill(Guid evaluationId, EvaluationSuccessState skillSuccess,
            CancellationToken cancellationToken)
        {
            var result = await _evaluationService.EnglishSkillSuccess(evaluationId, skillSuccess, await GetCurrentUser(), cancellationToken);
            return Ok(result);
        }
    }
}