using System;

namespace HRTech.Application.Services.Company.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public string CompanyName { get; set; }
            
            public sealed class LogoCompany
            {
                public Guid FileGuid { get; set; }
                public string FileName { get; set; }
                public string FileType { get; set; }
                public byte[] Content { get; set; }
        
                public Guid CompanyId { get; set; }
            }
            
            public LogoCompany? Logo { get; set; }
            
            public sealed class Address
            {
                public string Country { get; set; }
                public string City { get; set; }
                public string Street { get; set; }
                public string HouseNumber { get; set; }
            }
            public Address CompanyAddress { get; set; }
            
            public sealed class ExcelFileUsers
            {
                public Guid FileGuid { get; set; }
                public string FileName { get; set; }
                public string FileType { get; set; }
                public byte[] Content { get; set; }
            }
            public ExcelFileUsers CompanyExcelFileUsers { get; set; }
        }

        public sealed class Response
        {
            public Guid Id { get; set; }
        }
    }
}