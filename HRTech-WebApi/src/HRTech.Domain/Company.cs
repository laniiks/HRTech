using System;
using System.Collections.Generic;
using Common.Enums;
using HRTech.Domain.Shared;
#pragma warning disable 8632

namespace HRTech.Domain
{
    public class Company : MutableEntity<Guid>
    {
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public CompanyState State { get; set; }
        public virtual ICollection<ApplicationUser> Employees { get; set; }
        public virtual Image? Image { get; set; }
        public virtual Address Address { get; set; }
        public virtual ExcelFileUsers ExcelFileUsers { get; set; }
        public virtual ICollection<Grade> GradesCollection { get; set; }
    }
}