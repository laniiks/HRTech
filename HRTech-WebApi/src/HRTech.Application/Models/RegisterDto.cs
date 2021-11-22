using System;
using Common.Enums;

namespace HRTech.Application.Models
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public Guid CompanyId { get; set; }
        public string? ExpertUserState { get; set; }
    }
}