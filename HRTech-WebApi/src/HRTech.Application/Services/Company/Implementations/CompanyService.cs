using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Enums;
using HRTech.Application.Abstractions;
using HRTech.Application.Services.Company.Contracts;
using HRTech.Application.Services.Company.Interfaces;
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
        private readonly IRepository<Domain.Address> _addressRepository;

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
            IRepository<Domain.Address> addressRepository)
        {
            _companyRepository = companyRepository;
            _userManager = userManager;
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
            _imageRepository = imageRepository;
            _addressRepository = addressRepository;
        }

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.Company company = null;
                company = new Domain.Company
                {
                    CompanyName = request.CompanyName,
                    State = CompanyState.New,
                    CreatedDateTime = DateTime.UtcNow,
                    Image = request.Logo != null
                        ? new Domain.Image()
                        {
                            FileGuid = request.Logo.FileGuid,
                            FileName = request.Logo.FileName,
                            FileType = request.Logo.FileType,
                            Content = request.Logo.Content,
                            CreatedDateTime = DateTime.UtcNow
                        }
                        : new Domain.Image(),
                    Address = new Domain.Address
                    {
                        Country = request.CompanyAddress.Country,
                        City = request.CompanyAddress.City,
                        Street = request.CompanyAddress.Street,
                        HouseNumber = request.CompanyAddress.HouseNumber,
                        CreatedDateTime = DateTime.UtcNow
                    }
                };

                await _companyRepository.Add(company, cancellationToken);
                await _companyRepository.SaveChanges(cancellationToken);
                _logger.LogInformation("Добавлена компания {@CompanyStruct}", new
                {
                    Id = company.Id,
                    CompanyName = company.CompanyName,
                    State = company.State,
                    CreatedDateTime = company.CreatedDateTime
                });
                return new Create.Response
                {
                    Id = company.Id
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при создании компании");
                throw new Exception( "Произошла ошибка при создании компании", e);
            }
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var company = await GetCompanyAndCheckForNull(id);
                
                if (company.Image != null)
                {
                    await _imageRepository.Delete(company.Image, cancellationToken);
                }

                await _addressRepository.Delete(company.Address, cancellationToken);
                await _companyRepository.Delete(company, cancellationToken);
                await _companyRepository.SaveChanges(cancellationToken);
                _logger.LogInformation("Удалена компания {@CompanyStruct}", new
                {
                    Id = company.Id,
                    CompanyName = company.CompanyName,
                    State = company.State,
                    CreatedDateTime = company.CreatedDateTime
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при удалении компании");
                throw new Exception( "Произошла ошибка при удалении компании", e);
            }
            return true;
        }

        public async Task<Get.Response> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var company = await GetCompanyAndCheckForNull(id);


                return new Get.Response
                {
                    Id = company.Id,
                    CompanyName = company.CompanyName,
                    Employees = company.Employees != null
                        ? company.Employees.Select(x => new Get.Response.Employee()
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Patronymic = x.Patronymic,
                            PhoneNumber = x.PhoneNumber,
                            UserName = x.UserName
                        }).ToList()
                        : new List<Get.Response.Employee>(),
                    Logo = company.Image != null ?
                        new Get.Response.LogoCompany
                        {
                            FileGuid = company.Image.FileGuid,
                            Content = company.Image.Content,
                            FileName = company.Image.FileName
                        } : new Get.Response.LogoCompany(),
                    CompanyAddress = new Get.Response.Address()
                    {
                        Country = company.Address.Country,
                        City = company.Address.City,
                        Street = company.Address.Street,
                        HouseNumber = company.Address.HouseNumber
                    }
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при получении компании");
                throw new Exception( "Произошла ошибка при получении компании", e);
            }
        }

        public async Task<GetAll.Response> GetAllCompany(CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAll(cancellationToken);

            return new GetAll.Response
            {
                Companies = company.Select(com => new Get.Response
                {
                    Id = com.Id,
                    CompanyName = com.CompanyName,
                    Employees = com.Employees != null
                        ? com.Employees.Select(x => new Get.Response.Employee()
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Patronymic = x.Patronymic,
                            PhoneNumber = x.PhoneNumber,
                            UserName = x.UserName
                        }).ToList()
                        : new List<Get.Response.Employee>(),
                    CountEmployee = com.Employees?.Count ?? 0,
                    Logo = com.Image != null ?
                        new Get.Response.LogoCompany
                        {
                            FileGuid = com.Image.FileGuid,
                            Content = com.Image.Content,
                            FileName = com.Image.FileName
                        } : new Get.Response.LogoCompany()
                })
            };
        }

        public async Task<Edit.Response> EditCompany(Edit.Request request, CancellationToken cancellationToken)
        {
            try
            {
                var company = await GetCompanyAndCheckForNull(request.id);

                UpdateLogo(company, request, cancellationToken);

                company.CompanyName = request.CompanyName;
                company.UpdateDateTime = DateTime.UtcNow;
                
                await _companyRepository.Update(company, cancellationToken);
                await _companyRepository.SaveChanges(cancellationToken);
                return new Edit.Response
                {
                    Id = company.Id
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при редактировании компании");
                throw new Exception( "Произошла ошибка при редактировании компании", e);
            }

        }

        private void UpdateLogo(Domain.Company company, Edit.Request request, CancellationToken cancellationToken)
        {
            if (request.Logo !=null)
            {
                company.Image.FileName = request.Logo.FileName;
                company.Image.FileType = request.Logo.FileType;
                company.Image.FileGuid = request.Logo.FileGuid;
                company.Image.Content = request.Logo.Content;
            }
        }

        private async Task<Domain.Company> GetCompanyAndCheckForNull(Guid id)
        {
            var company = await _companyRepository.GetByIdGuid(id);
            if (company == null)
            {
                throw new Exception($"Компания с Id {id} не была найдена");
            }

            return company;
        }

    }
}