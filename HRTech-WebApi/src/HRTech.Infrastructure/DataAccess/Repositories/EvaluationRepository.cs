using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Abstractions;
using HRTech.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRTech.Infrastructure.DataAccess.Repositories
{
    public class EvaluationRepository : BaseRepository<Evaluation>, IEvaluationRepository
    {
        public EvaluationRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ICollection<Evaluation>> GetAllEvaluationForUser(string userId, CancellationToken cancellationToken)
        {
            return await _databaseContext.Evaluations
                .Where(x => x.ApplicationUserId == userId)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<ICollection<Evaluation>> GetAllEvaluationForExpertUser(string userId, CancellationToken cancellationToken)
        {
            return await _databaseContext.Evaluations
                .Where(x => x.ApplicationUserIdExpertSoftSkills == userId || 
                            x.ApplicationUserIdExpertHardSkills == userId || 
                            x.ApplicationUserIdExpertEnglishSkills == userId)
                .ToArrayAsync(cancellationToken);        
        }

        public async Task<ICollection<Evaluation>> GetAllEvalutionInCompany(Guid companyId, CancellationToken cancellationToken)
        {
            return await _databaseContext.Evaluations
                .Where(x => x.CompanyId == companyId)
                .ToArrayAsync(cancellationToken);        
        }
    }
}