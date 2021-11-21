using HRTech.Domain.Shared;

namespace HRTech.Domain
{
    public class FileTemplate : Entity<int>
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}