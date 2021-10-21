using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Enums;
using HRTech.Domain;

namespace HRTech.Application.Abstractions
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<ICollection<Company>> GetAll(CompanyState state, CancellationToken cancellationToken);
    }
}