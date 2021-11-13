using System;

namespace HRTech.Application.Models
{
    public class GradeDto
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Title { get; set; }
        public Guid CompanyId { get; set; }
    }
}