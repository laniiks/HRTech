using System;

namespace HRTech.WebApi.Models.Company
{
    public class EditCompanyRequest
    {
        public Guid id { get; set; }
        public string CompanyName { get; set; }
    }
}