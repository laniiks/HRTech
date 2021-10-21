using System;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Services.Company.Contracts;
using HRTech.Application.Services.Company.Interfaces;
using HRTech.WebApi.Models.Company;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Controllers.Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCompany(CreateCompanyRequest request,
            CancellationToken cancellationToken)
        {
            Create.Response response;
            try
            {
                response = await _companyService.Create(new Create.Request
                {
                    CompanyName = request.CompanyName
                }, cancellationToken);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return BadRequest(e.Message);
            }

            return Created($"api/company/{response.Id}", new {response.Id});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCompany(Guid companyId, CancellationToken cancellationToken)
        {
            var result = await _companyService.Delete(companyId, cancellationToken);

            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetByIdCompany(Guid companyId, CancellationToken cancellationToken)
        {
            var result = await _companyService.GetById(companyId, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditCompeny(EditCompanyRequest request, CancellationToken cancellationToken)
        {
            Edit.Response response;
            try
            {
                response = await _companyService.EditCompany(new Edit.Request
                {
                    id = request.id,
                    CompanyName = request.CompanyName
                }, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Created($"api/company/{response.Id}", new {response.Id});

        }

        [HttpGet("GetAllCompany")]
        public async Task<IActionResult> GetAllCompany(CancellationToken cancellationToken)
        {
            var result = await _companyService.GetAllCompany(cancellationToken);
            return Ok(result);
        }


    }
}