using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Models.Company
{
    public class CreateCompanyRequest
    {
        public string CompanyName { get; set; }
        public IFormFile File { get; set; }
    }
}