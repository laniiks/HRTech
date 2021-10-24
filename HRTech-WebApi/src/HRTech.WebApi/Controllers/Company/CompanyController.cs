using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HRTech.Application.Services.Company.Contracts;
using HRTech.Application.Services.Company.Interfaces;
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

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCompany([FromForm]CreateCompanyRequest request,
            CancellationToken cancellationToken)
        {
            Create.Response response;
            
            try
            {
                var logo = GetFileInfo(request.Logo);
                var excelFile = GetFileInfo(request.ExcelFileUsers);

                response = await _companyService.Create(new Create.Request
                {
                    CompanyName = request.CompanyName,
                    Logo = logo != null ? new Create.Request.LogoCompany
                    {
                        FileGuid = logo.FileGuid,
                        FileName = logo.FileName,
                        FileType = logo.FileType,
                        Content = logo.Content
                    } : new Create.Request.LogoCompany(),
                    CompanyAddress = new Create.Request.Address
                    {
                        Country = request.Country,
                        City = request.City,
                        Street = request.Street,
                        HouseNumber = request.HouseNumber
                    },
                    CompanyExcelFileUsers = excelFile != null ? new Create.Request.ExcelFileUsers
                    {
                        FileGuid = excelFile.FileGuid,
                        FileName = excelFile.FileName,
                        FileType = excelFile.FileType,
                        Content = excelFile.Content
                    } : new Create.Request.ExcelFileUsers()
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
        public async Task<IActionResult> EditCompany([FromForm]EditCompanyRequest request, CancellationToken cancellationToken)
        {
            Edit.Response response;
            try
            {
                if (request.File is {Length: > 0})
                {
                    var fileName = request.File.FileName;
                    var fileExtension = Path.GetExtension(fileName);
                    await using var target = new MemoryStream();
                    target.Position = 0;
                    await request.File.CopyToAsync(target, cancellationToken);
                    response = await _companyService.EditCompany(new Edit.Request
                    {
                        id = request.id,
                        CompanyName = request.CompanyName,
                        Logo =  new Edit.Request.LogoCompany
                        {
                            FileGuid = Guid.NewGuid(),
                            FileName = fileName,
                            FileType = fileExtension,
                            Content = target.ToArray()
                        }
                    }, cancellationToken);
                }
                else
                {
                    response = await _companyService.EditCompany(new Edit.Request
                    {
                        id = request.id,
                        CompanyName = request.CompanyName
                    }, cancellationToken);
                }
                
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