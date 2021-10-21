using System;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Services.Company.Contracts;

namespace HRTech.Application.Services.Company.Interfaces
{
    public interface ICompanyService
    {
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
        Task<Get.Response> GetById(Guid id, CancellationToken cancellationToken);
        Task<Edit.Response> EditCompany(Edit.Request request, CancellationToken cancellationToken);

    }
}