using System;
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
    public class ApplicationUserRepository : BaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<ICollection<ApplicationUser>> GetAllExpertUserInCompany(string userId, Guid companyId, ExpertUserState expertUserState, CancellationToken cancellationToken)
        {
            return await _databaseContext.ApplicationUsers
                .AsNoTracking()
                .Where(x => x.CompanyId == companyId && x.ExpertUserState == expertUserState && x.Id != userId)
                .ToArrayAsync(cancellationToken);
        }
    }
}