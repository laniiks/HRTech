using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Models;

namespace HRTech.Application.Services.Company.Interfaces
{
    public interface ICompanyService
    {
        Task<Guid> Create(CompanyDto companyDto, CancellationToken cancellationToken);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
        Task<CompanyDto> GetById(Guid id, CancellationToken cancellationToken);
        Task<ICollection<Domain.Company>> GetNewOrActiveCompany(bool isNewCompany, CancellationToken cancellationToken);
        Task<ICollection<Domain.Company>> GetAll(CancellationToken cancellationToken);

        Task<Guid> EditCompany(CompanyDto companyDto, CancellationToken cancellationToken);
        Task<bool> ActiveCompany(Guid id, bool isRegisterUser, CancellationToken cancellationToken);
    }
}