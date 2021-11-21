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
    public class GradeRepository : BaseRepository<Grade>, IGradeRepository
    {
        public GradeRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ICollection<Grade>> GetAllGradeInCompany(Guid companyId, CancellationToken cancellationToken)
        {
            return await _databaseContext.Grades
                .OrderByDescending(x => x.Id)
                .Where(x => x.CompanyId == companyId)
                .ToArrayAsync(cancellationToken);
        }
    }
}