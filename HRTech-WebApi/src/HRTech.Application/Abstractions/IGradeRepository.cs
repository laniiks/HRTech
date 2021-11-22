using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Domain;

namespace HRTech.Application.Abstractions
{
    public interface IGradeRepository : IRepository<Grade>
    {
        Task<ICollection<Grade>> GetAllGradeInCompany(Guid companyId, CancellationToken cancellationToken);
        Task<Grade> GetNextGrade(Guid companyId, int currentGradeId, CancellationToken cancellationToken);
    }
}