using System;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Services.Address.Contracts;

namespace HRTech.Application.Services.Address.Interfaces
{
    public interface IAddressService
    {
        Task<Guid> UpdateAddressInCompany(Edit.Request request, CancellationToken cancellationToken);
    }
}