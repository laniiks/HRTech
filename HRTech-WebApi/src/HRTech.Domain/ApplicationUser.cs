using System;
using System.Collections.Generic;
using Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace HRTech.Domain
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        
        public Guid? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<PersonalDevelopmentPlan> PersonalDevelopmentPlans { get; set; }
        
        public int? GradeId { get; set; }
        public virtual Grade Grades { get; set; }
        public ExpertUserState ExpertUserState { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public bool IsDirector { get; set; }
        
        public virtual Image? Photo { get; set; }
    }
}