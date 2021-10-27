using System;
using System.Threading.Tasks;
using HRTech.Application.Abstractions;
using HRTech.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRTech.Infrastructure.DataAccess.Repositories
{
    public class PersonalDevelopmentPlanRepository : BaseRepository<PersonalDevelopmentPlan>, IPersonalDevelopmentPlanRepository
    {
        public PersonalDevelopmentPlanRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<PersonalDevelopmentPlan> GetByFileGuid(Guid fileGuid)
        {
            return await _databaseContext.PersonalDevelopmentPlans
                .SingleOrDefaultAsync(x => x.FileGuid == fileGuid);
        }
    }
}