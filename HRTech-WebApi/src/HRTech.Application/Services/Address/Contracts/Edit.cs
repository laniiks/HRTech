using System;

namespace HRTech.Application.Services.Address.Contracts
{
    public sealed class Edit
    {
        public class Request
        {
            public Guid Id { get; set; }
            public sealed class Address
            {
                public string Country { get; set; }
                public string City { get; set; }
                public string Street { get; set; }
                public string HouseNumber { get; set; }
            }
            public Address CompanyAddress { get; set; }
        }
    }
}