using System;

namespace HRTech.WebApi.Models.File
{
    public class UploadFileRequest
    {
        public Guid FileGuid { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Content { get; set; }
    }
}