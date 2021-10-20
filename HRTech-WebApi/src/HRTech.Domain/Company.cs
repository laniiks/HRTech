using System;
using System.Collections;
using System.Collections.Generic;
using HRTech.Domain.Shared;

namespace HRTech.Domain
{
    public class Company : MutableEntity<Guid>
    {
        public string CompanyName { get; set; }
        public virtual ICollection<ApplicationUser> Employees { get; set; }
    }
}