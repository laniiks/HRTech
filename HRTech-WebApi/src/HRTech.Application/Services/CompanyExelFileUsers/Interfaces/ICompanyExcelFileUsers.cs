using System;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Models;

namespace HRTech.Application.Services.CompanyExelFileUsers.Interfaces
{
    public interface ICompanyExcelFileUsers
    {
        Task<Guid> AddFileExcelUsersInCompany(Guid id, FileDto fileDto, CancellationToken cancellationToken);
    }
}