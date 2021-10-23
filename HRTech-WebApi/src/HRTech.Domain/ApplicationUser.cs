using System;
using Microsoft.AspNetCore.Identity;

namespace HRTech.Domain
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}