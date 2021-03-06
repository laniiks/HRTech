using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRTech.Application.Models;
using HRTech.Application.Services.Company.Interfaces;
using HRTech.Application.Services.CompanyExelFileUsers.Interfaces;
using HRTech.WebApi.Models.Company;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Controllers.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyExcelFileUsers _companyExcelFileUsers;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper, ICompanyExcelFileUsers companyExcelFileUsers)
        {
            _companyService = companyService;
            _mapper = mapper;
            _companyExcelFileUsers = companyExcelFileUsers;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _companyService.GetById(id, cancellationToken);
            return Ok(result);
        }
        
        [HttpGet("GetNewOrActiveCompany")]
        public async Task<IActionResult> GetNewOrActiveCompany(bool isNewCompany, CancellationToken cancellationToken)
        {
            var result = await _companyService.GetNewOrActiveCompany(isNewCompany, cancellationToken);
            return Ok(result);
        }
        
        [HttpGet("All")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _companyService.GetAll(cancellationToken);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyRequest createCompanyRequest, CancellationToken cancellationToken)
        {
            var result = await _companyService
                .Create(_mapper.Map<CompanyDto>(createCompanyRequest), cancellationToken);
            return Ok(result);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _companyService.Delete(id, cancellationToken);
            return Ok(result);
        }

        [HttpPut("EditCompany")]
        public async Task<IActionResult> EditCompany(EditCompanyRequest editCompanyRequest,
            CancellationToken cancellationToken)
        {
            var result =
                await _companyService.EditCompany(_mapper.Map<CompanyDto>(editCompanyRequest), cancellationToken);
            return Ok(result);
        }

        [HttpPut("ActiveCompany")]
        public async Task<IActionResult> ActiveCompany(Guid id, bool isRegisterUser)
        {
            var result = await _companyService.ActiveCompany(id, isRegisterUser, CancellationToken.None);
            return Ok(result);
        }
        
        [HttpPut("RestoreCompany")]
        public async Task<IActionResult> RestoreCompany(Guid id, CancellationToken cancellationToken)
        {
            var result = await _companyService.RestoreCompany(id, cancellationToken);
            return Ok(result);
        }

        [HttpPost("AddFileExcelUsersInCompany")]
        public async Task<IActionResult> AddFileExcelUsersInCompany(Guid id, IFormFile formFile,
            CancellationToken cancellationToken)
        {
            var file = GetFileInfo(formFile);
            var result = await _companyExcelFileUsers.AddFileExcelUsersInCompany(id, file, cancellationToken);
            return Ok(result);
        }
        
        [HttpDelete("RejectCompany/{companyId}")]
        public async Task<IActionResult> RejectCompany(Guid companyId)
        {
            var result = await _companyService.RejectCompany(companyId, CancellationToken.None);
            return Ok(result);
        }

        [HttpGet("GetCountNewCompany")]
        public async Task<IActionResult> GetCountNewCompany(CancellationToken cancellationToken)
        {
            var result = await _companyService.GetCountNewCompany(cancellationToken);
            return Ok(result);
        }

    }
}