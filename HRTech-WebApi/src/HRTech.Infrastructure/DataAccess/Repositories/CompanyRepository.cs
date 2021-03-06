using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Enums;
using HRTech.Application.Abstractions;
using HRTech.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRTech.Infrastructure.DataAccess.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ICollection<Company>> GetAll(CompanyState state, CancellationToken cancellationToken)
        {
            if (state == CompanyState.New)
            {
                return await _databaseContext.Companies
                    .OrderByDescending(d => d.CreatedDateTime)
                    .Include(x=>x.Employees)
                    .Include(x=>x.Image)
                    .Where(x=>x.State == state)
                    .ToArrayAsync(cancellationToken);
            }
            else
            {
                return await _databaseContext.Companies
                    .OrderByDescending(d => d.CreatedDateTime)
                    .Include(x=>x.Employees)
                    .Include(x=>x.Image)
                    .Where(x=>x.State == state || x.State == CompanyState.Delete)
                    .ToArrayAsync(cancellationToken);
            }
        }

        public async Task<int> GetCountNewCompany(CancellationToken cancellationToken)
        {
            return await _databaseContext.Companies
                .Where(x => x.State == CompanyState.New)
                .CountAsync(cancellationToken);
        }
    }
}