using System;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Domain;

namespace HRTech.Application.Abstractions
{
    public interface IPersonalDevelopmentPlanRepository : IRepository<PersonalDevelopmentPlan>
    {
        Task<PersonalDevelopmentPlan> GetByFileGuid(Guid fileGuid);
    }
}