using System;

namespace HRTech.Application.Models
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}