using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRTech.WebApi.Models.Company
{
    public class CreateCompanyRequest
    {
        public string CompanyName { get; set; }
        public IFormFile Logo { get; set; }
        public IFormFile ExcelFileUsers { get; set; }
        
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}