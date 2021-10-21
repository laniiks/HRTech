using System;

namespace HRTech.Application.Services.Company.Contracts
{
    public sealed class Edit
    {
        public sealed class Request
        {
            public Guid id { get; set; }
            public string CompanyName { get; set; }
        }

        public sealed class Response
        {
            public Guid Id { get; set; }
        }
        
    }
}