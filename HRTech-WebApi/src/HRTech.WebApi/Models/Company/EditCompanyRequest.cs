using System;
using Microsoft.AspNetCore.Http;

namespace HRTech.WebApi.Models.Company
{
    public class EditCompanyRequest
    {
        public Guid id { get; set; }
        public string CompanyName { get; set; }
        public IFormFile File { get; set; }
    }
}