using System;
using System.Collections.Generic;
using Common.Enums;
using HRTech.Domain.Shared;

namespace HRTech.Domain
{
    public class Company : MutableEntity<Guid>
    {
        public string CompanyName { get; set; }
        public CompanyState State { get; set; }
        public virtual ICollection<ApplicationUser> Employees { get; set; }
    }
}