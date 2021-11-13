using System;
using System.Collections.Generic;
using Common.Enums;

namespace HRTech.Application.Models
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public string CompanyName { get; set; }
        public CompanyState State { get; set; }
        public virtual ICollection<UserDto> Employees { get; set; }
        public virtual FileDto Image { get; set; }
        public virtual AddressDto Address { get; set; }
        public virtual FileDto ExcelFileUsers { get; set; }
        public virtual ICollection<GradeDto> GradesCollection { get; set; }
    }
}