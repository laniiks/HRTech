using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Enums;
using HRTech.Domain;

namespace HRTech.Application.Abstractions
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ICollection<ApplicationUser>> GetAllExpertUserInCompany(string userId, Guid companyId, ExpertUserState expertUserState,
            CancellationToken cancellationToken);
    }
}