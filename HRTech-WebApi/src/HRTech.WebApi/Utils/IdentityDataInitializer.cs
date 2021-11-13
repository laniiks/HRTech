using System;
using HRTech.Application.Common;
using HRTech.Domain;
using Microsoft.AspNetCore.Identity;

namespace HRTech.WebApi.Utils
{
    public class IdentityDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        
        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new ApplicationUser { UserName = "admin@test.ru", Email = "admin@test.ru", FirstName = "Ivan", LastName = "Ivanov", CompanyId = Guid.Parse("71798C1D-78B1-4859-B439-E6A26CB24086"), GradeId = 3};
                var result = userManager.CreateAsync(user, "adminA1+").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, RolesConst.Admin).Wait();
                }
            }

            if (userManager.FindByNameAsync("user").Result == null)
            {
                var user = new ApplicationUser { UserName = "user@test.ru", Email = "user@test.ru", FirstName = "Piter", LastName = "Petrov", CompanyId = Guid.Parse("71798C1D-78B1-4859-B439-E6A26CB24086"), GradeId = 2};
                var result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, RolesConst.User).Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(RolesConst.User).Result)
            {
                var role = new IdentityRole { Name = RolesConst.User };
                var roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(RolesConst.Admin).Result)
            {
                var role = new IdentityRole { Name = RolesConst.Admin };
                var roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}