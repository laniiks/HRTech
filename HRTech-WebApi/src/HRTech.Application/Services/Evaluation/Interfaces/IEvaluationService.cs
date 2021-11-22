using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    }
    
    public class GetAll
    {
        public class Response
        {
            public IEnumerable<EvaluationDto> Evaluation { get; set; }
        }
    }
}