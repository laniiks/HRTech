using HRTech.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HRTech.Infrastructure.DataAccess
{
    public class DatabaseContextInstaller
    {
        public static void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>();
        }
    }
}