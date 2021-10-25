using System;
using System.Collections.Generic;
using HRTech.Domain.Shared;

namespace HRTech.Domain
{
    public class ExcelFileUsers : Entity<Guid>
    {
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Content { get; set; }
        
        public virtual ICollection<Company> Company { get; set; }
    }
}