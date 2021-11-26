using System;
using Common.Enums;

namespace HRTech.Application.Models
{
    public class UserDto
    {
        public string ApplicationUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        
        public Guid CompanyId { get; set; }
        public int GradeId { get; set; }
        public virtual GradeDto Grades { get; set; }
        public ExpertUserState ExpertUserState { get; set; }
        public bool IsDirector { get; set; }
        public virtual FileDto? Photo { get; set; }
    }
}