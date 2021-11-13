using System;
using HRTech.Domain.Shared;

namespace HRTech.Domain
{
    public class Grade: Entity<int>
    {
        public string Title { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}