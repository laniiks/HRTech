using System;

namespace HRTech.Application.Services.Company.Contracts
{
    public static class Create
    {
        public sealed class Request
        {
            public string CompanyName { get; set; }
        }

        public sealed class Response
        {
            public Guid Id { get; set; }
        }
    }
}