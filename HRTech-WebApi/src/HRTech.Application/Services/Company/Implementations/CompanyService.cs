using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Enums;
using HRTech.Application.Abstractions;
using HRTech.Application.Models;
using HRTech.Application.Services.Company.Interfaces;
using HRTech.Application.Services.Evaluation.Interfaces;
using HRTech.Application.Services.User.Interfaces;
using HRTech.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HRTech.Application.Services.Company.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IRepository<Domain.Image> _imageRepository;
        private readonly IRepository<Domain.ExcelFileUsers> _excelFileUsersRepository;
        private readonly IGeneratePassword _generatePassword;
        private readonly IGetUsersFromExcelFile _getUsersFromExcelFile;

        private readonly IRepository<Domain.Address> _addressRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IEvaluationRepository _evaluationRepository;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CompanyService(
            ICompanyRepository companyRepository, 
            UserManager<ApplicationUser> userManager, 
            IUserService userService, 
            ILogger<CompanyService> logger, 
            IMapper mapper, 
            IRepository<Domain.Image> imageRepository, 
            IRepository<Domain.Address> addressRepository, 
            IRepository<ExcelFileUsers> excelFileUsersRepository, 
            IGeneratePassword generatePassword, 
            IGetUsersFromExcelFile getUsersFromExcelFile, IApplicationUserRepository applicationUserRepository, IEvaluationRepository evaluationRepository)
        {
            _companyRepository = companyRepository;
            _userManager = userManager;
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
            _imageRepository = imageRepository;
            _addressRepository = addressRepository;
            _excelFileUsersRepository = excelFileUsersRepository;
            _generatePassword = generatePassword;
            _getUsersFromExcelFile = getUsersFromExcelFile;
            _applicationUserRepository = applicationUserRepository;
            _evaluationRepository = evaluationRepository;
        }
        
        public async Task<Guid> Create(CompanyDto companyDto, CancellationToken cancellationToken)
        {
            try
            {
                ValidateExtension(companyDto.Image.FileType);
                var company = _mapper.Map<Domain.Company>(companyDto);
                await _companyRepository.Add(company, cancellationToken);
                await _companyRepository.SaveChanges(cancellationToken);
                return company.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        private void ValidateExtension(string fileDtoFileType)
        {
            var allowedExtensions = new List<string> {".jpg", ".jpeg", ".png"};
            if (!allowedExtensions.Contains(fileDtoFileType))
            {
                throw new Exception("Файл не соответствует");
            }
        }
        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _companyRepository.GetByIdGuid(id);
                if (company == null)
                {
                    throw new Exception("Не найдено");
                }

                company.State = CompanyState.Delete;
                foreach (var employee in company.Employees)
                {
                    var user = await _applicationUserRepository.GetById(employee.Id);
                    user.EmailConfirmed = false;
                    await _applicationUserRepository.Update(user);
                    await _applicationUserRepository.SaveChanges(cancellationToken);
                }

                await _companyRepository.Update(company, cancellationToken);
                await _companyRepository.SaveChanges(cancellationToken);
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> RestoreCompany(Guid companyId, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _companyRepository.GetByIdGuid(companyId);
                if (company == null)
                {
                    throw new Exception("Не найдено");
                }

                company.State = CompanyState.Active;
                foreach (var employee in company.Employees)
                {
                    var user = await _applicationUserRepository.GetById(employee.Id);
                    user.EmailConfirmed = true;
                    await _applicationUserRepository.Update(user);
                    await _applicationUserRepository.SaveChanges(cancellationToken);
                }

                await _companyRepository.Update(company, cancellationToken);
                await _companyRepository.SaveChanges(cancellationToken);
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }        
        }

        public async Task<CompanyDto> GetById(Guid id, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdGuid(id);
            if (company == null)
            {
                throw new Exception("Не найдено");
            }

            return _mapper.Map<CompanyDto>(company);
        }

        public async Task<ICollection<Domain.Company>> GetNewOrActiveCompany(bool isNewCompany, CancellationToken cancellationToken)
        {
            ICollection<CompanyDto> company;
            if (isNewCompany)
            {
                company = _mapper.Map<ICollection<CompanyDto>>(await _companyRepository.GetAll(CompanyState.New, cancellationToken));
            }
            else
            {
                company = _mapper.Map<ICollection<CompanyDto>>(await _companyRepository.GetAll(CompanyState.Active, cancellationToken));
            }

            if (company == null)
            {
                throw new Exception("Не найдено");
            }

            return _mapper.Map<ICollection<Domain.Company>>(company);
        }

        public async Task<ICollection<Domain.Company>> GetAll(CancellationToken cancellationToken)
        {
            var company = _mapper.Map<ICollection<CompanyDto>>(await _companyRepository.GetAll(cancellationToken));

            if (company == null)
            {
                throw new Exception("Не найдено");
            }

            return _mapper.Map<ICollection<Domain.Company>>(company);
        }

        public async Task<Guid> EditCompany(CompanyDto companyDto, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _companyRepository.GetByIdGuid(companyDto.Id);
                if (company == null)
                {
                    throw new Exception("Не найдено");
                }

                if (company.Image != null)
                {
                    await _imageRepository.Delete(company.Image, cancellationToken);
                }

                if (company.ExcelFileUsers != null)
                {
                    await _excelFileUsersRepository.Delete(company.ExcelFileUsers, cancellationToken);
                }

                if (company.Address != null)
                {
                    await _addressRepository.Delete(company.Address, cancellationToken);
                }
                _mapper.Map(companyDto, company);
                await _companyRepository.Update(company, cancellationToken);
                await _companyRepository.SaveChanges(cancellationToken);
                return company.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> ActiveCompany(Guid id, bool isRegisterUser, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdGuid(id);
            if (company == null)
            {
                throw new Exception("Не найдено");
            }

            company.State = CompanyState.Active;
            company.UpdateDateTime = DateTime.UtcNow;

            await _companyRepository.Update(company, cancellationToken);
            await _companyRepository.SaveChanges(cancellationToken);

            if (isRegisterUser && company.ExcelFileUsers != null)
            {
                var listUsers = _getUsersFromExcelFile.GetUsersFromExcelFile(company.ExcelFileUsers, company.Id);
                await _userService.CreateRange(listUsers);
            }
            
            return true;
        }

        public async Task<bool> RejectCompany(Guid companyId, CancellationToken cancellationToken)
        {
            try
            {
                var company = await _companyRepository.GetByIdGuid(companyId);
                if (company == null)
                {
                    throw new Exception("Не найдено");
                }

                if (company.Address != null)
                {
                    await _addressRepository.Delete(company.Address, cancellationToken);
                }

                if (company.Image != null)
                {
                    await _imageRepository.Delete(company.Image, cancellationToken);
                }

                if (company.ExcelFileUsers != null)
                {
                    await _excelFileUsersRepository.Delete(company.ExcelFileUsers, cancellationToken);
                }
                await _companyRepository.Delete(company, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return true;
        }

        public async Task<int> GetCountNewCompany(CancellationToken cancellationToken)
        {
            var countNewCompany = await _companyRepository.GetCountNewCompany(cancellationToken);
            return countNewCompany;
        }
    }
}