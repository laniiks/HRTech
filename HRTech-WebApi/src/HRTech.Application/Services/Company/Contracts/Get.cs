using System;
using System.Collections.Generic;

namespace HRTech.Application.Services.Company.Contracts
{
    public static class Get
    {
        public sealed class Request
        {
            public Guid Id { get; set; }
        }

        public sealed class Response
        {
            public Guid Id { get; set; }
            public string CompanyName { get; set; }
            public sealed class Employee
            {
                public string Id { get; set; }
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string Patronymic { get; set; }
                public string PhoneNumber { get; set; }
                public string UserName { get; set; }
            }
            public ICollection<Employee> Employees { get; set; }
            public int CountEmployee { get; set; }
        }
    }

    
}