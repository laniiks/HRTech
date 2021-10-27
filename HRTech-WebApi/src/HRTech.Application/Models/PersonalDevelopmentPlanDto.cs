using System;
using HRTech.Domain;

namespace HRTech.Application.Models
{
    public class PersonalDevelopmentPlanDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public string Title { get; set; }
        
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Content { get; set; }
        
        public Guid ApplicationUserId { get; set; }
    }
}