using System;

namespace HRTech.Application.Models
{
    public class FileDto
    {
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Content { get; set; }
    }
}