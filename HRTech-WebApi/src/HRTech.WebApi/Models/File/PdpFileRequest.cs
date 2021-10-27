using System;

namespace HRTech.WebApi.Models.File
{
    public class PdpFileRequest
    {
        public string Title { get; set; }
        
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Content { get; set; }
    }
}