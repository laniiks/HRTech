﻿using Microsoft.AspNetCore.Identity;

namespace HRTech.Domain
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}