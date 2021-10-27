using System;
using HRTech.Domain.Shared;

namespace HRTech.Domain
{
    public class PersonalDevelopmentPlan : Entity<Guid>
    {
        public string Title { get; set; }
        
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Content { get; set; }
        
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}