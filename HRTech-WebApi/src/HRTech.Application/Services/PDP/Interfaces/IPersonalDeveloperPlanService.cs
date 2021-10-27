using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Models;
using HRTech.Domain;

namespace HRTech.Application.Services.PDP.Interfaces
{
    public interface IPersonalDeveloperPlanService
    {
        Task<Guid> AddPdpdForUser(ApplicationUser user, PersonalDevelopmentPlanDto personalDevelopmentPlanDto,
            CancellationToken cancellationToken);

        Task<ICollection<PersonalDevelopmentPlanDto>> GetAllPdpForUser(ApplicationUser user, CancellationToken cancellationToken);

        Task<PersonalDevelopmentPlanDto> GetFileAsync(Guid fileGuid);
    }
}