using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Domain;

namespace HRTech.Application.Abstractions
{
    public interface IEvaluationRepository : IRepository<Evaluation>
    {
        Task<ICollection<Evaluation>> GetAllEvaluationForUser(string userId, CancellationToken cancellationToken);
        Task<ICollection<Evaluation>> GetAllEvaluationForExpertUser(string userId, CancellationToken cancellationToken);
        Task<ICollection<Evaluation>> GetAllEvalutionInCompany(Guid companyId, CancellationToken cancellationToken);

    }
}