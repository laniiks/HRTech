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

        public async Task<Grade> GetNextGrade(Guid companyId, int currentGradeId, CancellationToken cancellationToken)
        {
            return await _databaseContext.Grades
                .Where(x => x.CompanyId == companyId && x.Id > currentGradeId)
                .OrderBy(x => x.Id)
                .FirstAsync(cancellationToken);
            // return await _databaseContext.Grades
            //     .Where(x => x.CompanyId == companyId)
            //     .SkipWhile(i => i.Id != currentGradeId).Skip(1).FirstOrDefaultAsync(cancellationToken);
        }
    }
}