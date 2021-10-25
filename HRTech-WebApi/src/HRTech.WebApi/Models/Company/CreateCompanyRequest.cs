using HRTech.WebApi.Models.Address;
using HRTech.WebApi.Models.File;

namespace HRTech.WebApi.Models.Company
{
    public class CreateCompanyRequest
    {
        public string CompanyName { get; set; }
        public UploadFileRequest Image { get; set; }
        public UploadFileRequest ExcelFileUsers { get; set; }
        public AddressCreateRequest Address { get; set; }
    }
}