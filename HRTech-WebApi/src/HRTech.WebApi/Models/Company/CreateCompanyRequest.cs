using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HRTech.WebApi.Models.Address;
using HRTech.WebApi.Models.File;

namespace HRTech.WebApi.Models.Company
{
    public class CreateCompanyRequest
    {
        [Required]
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public UploadFileRequest Image { get; set; }
        public UploadFileRequest ExcelFileUsers { get; set; }
        [Required]
        public AddressCreateRequest Address { get; set; }
        public List<GradeCreateRequest> Grades { get; set; }
    }
}