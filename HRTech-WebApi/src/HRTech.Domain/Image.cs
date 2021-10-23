using System;
using HRTech.Domain.Shared;

namespace HRTech.Domain
{
    public class Image : Entity<Guid>
    {
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Content { get; set; }
        
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}