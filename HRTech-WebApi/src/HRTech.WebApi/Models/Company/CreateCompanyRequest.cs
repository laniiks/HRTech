using System.Collections.Generic;
using HRTech.WebApi.Models.Address;
using HRTech.WebApi.Models.File;
using Microsoft.Extensions.DependencyModel;

namespace HRTech.WebApi.Models.Company
{
    public class CreateCompanyRequest
    {
        public string CompanyName { get; set; }
        public UploadFileRequest Image { get; set; }
        public UploadFileRequest ExcelFileUsers { get; set; }
        public AddressCreateRequest Address { get; set; }
        public List<GradeCreateRequest> Grades { get; set; }
    }
}