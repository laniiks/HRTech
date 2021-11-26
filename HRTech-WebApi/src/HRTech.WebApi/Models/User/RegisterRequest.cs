using System;
using Common.Enums;

namespace HRTech.WebApi.Models.User
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsDirector { get; set; }
    }
}