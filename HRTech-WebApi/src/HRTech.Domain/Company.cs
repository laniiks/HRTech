﻿using System;
using System.Collections.Generic;
using Common.Enums;
using HRTech.Domain.Shared;
#pragma warning disable 8632

namespace HRTech.Domain
{
    public class Company : MutableEntity<Guid>
    {
        public string CompanyName { get; set; }
        public CompanyState State { get; set; }
        public virtual ICollection<ApplicationUser> Employees { get; set; }
        public virtual Image? Image { get; set; }
    }
}