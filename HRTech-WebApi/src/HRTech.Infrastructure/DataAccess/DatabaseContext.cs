using System;
using System.IO;
using HRTech.Domain;
using MassTransit.Futures.Contracts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HRTech.Infrastructure.DataAccess
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PersonalDevelopmentPlan> PersonalDevelopmentPlans { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<FileTemplate> FileTemplates { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseMySql(config.GetConnectionString("MySqlDb"), new MySqlServerVersion(new Version(8, 0, 11)));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}