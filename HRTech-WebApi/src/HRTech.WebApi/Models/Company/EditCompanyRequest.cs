using System;
using HRTech.WebApi.Models.Address;
using HRTech.WebApi.Models.File;
using Microsoft.AspNetCore.Http;

namespace HRTech.WebApi.Models.Company
{
    public class EditCompanyRequest
    {
        public Guid id { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public UploadFileRequest Image { get; set; }
        public UploadFileRequest ExcelFileUsers { get; set; }
        public AddressCreateRequest Address { get; set; }    }
}