using System;
using System.Collections.Generic;
using HRTech.Domain.Shared;

namespace HRTech.Domain
{
    public class Address : MutableEntity<Guid>
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}