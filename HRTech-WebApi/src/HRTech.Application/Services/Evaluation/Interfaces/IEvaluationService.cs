using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Enums;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Services.Evaluation.Interfaces
{
    public interface IEvaluationService
    {
        Task<EvaluationDto> GetByIdEvaluation(Guid evaluationId,
            CancellationToken cancellationToken);
        Task<GetAll.Response> GetAllResponseEvaluationForUser(ApplicationUser user,
            CancellationToken cancellationToken);
        Task<GetAll.Response> GetAllResponseEvaluationForExpertUser(ApplicationUser user,
            CancellationToken cancellationToken);

        Task<Guid> CreateEvaluation(EvaluationDto evaluationDto, ApplicationUser user, CancellationToken cancellationToken);
        Task<Guid> SoftSkillSuccess(Guid evaluationId, EvaluationSuccessState softSkillSuccess, ApplicationUser user, CancellationToken cancellationToken);
        Task<Guid> HardSkillSuccess(Guid evaluationId, EvaluationSuccessState softSkillSuccess, ApplicationUser user, CancellationToken cancellationToken);
        Task<Guid> EnglishSkillSuccess(Guid evaluationId, EvaluationSuccessState softSkillSuccess, ApplicationUser user,  CancellationToken cancellationToken);

    }
    
    public class GetAll
    {
        public class Response
        {
            public IEnumerable<EvaluationDto> Evaluation { get; set; }
        }
    }
}