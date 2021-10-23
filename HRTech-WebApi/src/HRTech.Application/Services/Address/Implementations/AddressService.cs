using System;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Abstractions;
using HRTech.Application.Services.Address.Contracts;
using HRTech.Application.Services.Address.Interfaces;

namespace HRTech.Application.Services.Address.Implementations
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Domain.Address> _addressRepository;
        private readonly ICompanyRepository _companyRepository;

        public AddressService(
            IRepository<Domain.Address> addressRepository, 
            ICompanyRepository companyRepository)
        {
            _addressRepository = addressRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Guid> UpdateAddressInCompany(Edit.Request request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdGuid(request.Id);

            if (company == null)
            {
                throw new Exception($"Компания с Id {request.Id} не была найдена");
            }
            
            company.Address.Country = request.CompanyAddress.Country;
            company.Address.City = request.CompanyAddress.City;
            company.Address.Street = request.CompanyAddress.Street;
            company.Address.HouseNumber = request.CompanyAddress.HouseNumber;

            await _companyRepository.Update(company, cancellationToken);
            await _companyRepository.SaveChanges(cancellationToken);
            
            return company.Id;
        }
    }
}
