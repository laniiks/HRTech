using System;

namespace HRTech.Application.Services.Company.Contracts
{
    public sealed class Edit
    {
        public sealed class Request
        {
            public Guid id { get; set; }
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
        }

        public sealed class Response
        {
            public Guid Id { get; set; }
        }
        
    }
}