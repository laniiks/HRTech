using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRTech.Application.Abstractions;
using HRTech.Application.Models;
using HRTech.Application.Services.Company.Interfaces;
using HRTech.Application.Services.CompanyExelFileUsers.Interfaces;
using HRTech.Domain;

namespace HRTech.Application.Services.CompanyExelFileUsers.Implementations
{
    public class CompanyExcelFileUsersService : ICompanyExcelFileUsers
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IRepository<ExcelFileUsers> _excelFileUsersRepository;
        private readonly IMapper _mapper;

        public CompanyExcelFileUsersService(IMapper mapper, IRepository<ExcelFileUsers> excelFileUsersRepository, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _excelFileUsersRepository = excelFileUsersRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Guid> AddFileExcelUsersInCompany(Guid id, FileDto fileDto, CancellationToken cancellationToken)
        {
            var excelFile = _mapper.Map<ExcelFileUsers>(fileDto);
            var company = await _companyRepository.GetByIdGuid(id);
            if (company == null)
            {
                throw new Exception("Компания не найдена");
            }

            company.ExcelFileUsers = excelFile;
            await _excelFileUsersRepository.Add(excelFile, cancellationToken);
            await _companyRepository.SaveChanges(cancellationToken);
            await _excelFileUsersRepository.SaveChanges(cancellationToken);

            return excelFile.Id;
        }
    }
}