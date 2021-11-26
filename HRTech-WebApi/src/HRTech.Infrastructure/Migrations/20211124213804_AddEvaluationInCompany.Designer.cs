﻿// <auto-generated />
using System;
using HRTech.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HRTech.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211124213804_AddEvaluationInCompany")]
    partial class AddEvaluationInCompany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("HRTech.Domain.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Street")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("HRTech.Domain.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ExpertUserState")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<int?>("GradeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDirector")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("Patronymic")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("GradeId")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HRTech.Domain.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("EvaluationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("TextComment")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("EvaluationId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("HRTech.Domain.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("char(36)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("ExcelFileUsersId")
                        .HasColumnType("char(36)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ExcelFileUsersId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("HRTech.Domain.Evaluation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ApplicationUserIdExpertEnglishSkills")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ApplicationUserIdExpertHardSkills")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ApplicationUserIdExpertSoftSkills")
                        .HasColumnType("varchar(255)");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("CurrentGradeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfEvaluation")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EnglishSkillSuccess")
                        .HasColumnType("int");

                    b.Property<int>("EvaluationState")
                        .HasColumnType("int");

                    b.Property<int>("HardSkillSuccess")
                        .HasColumnType("int");

                    b.Property<int?>("NextGradeId")
                        .HasColumnType("int");

                    b.Property<int>("SoftSkillSuccess")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ApplicationUserIdExpertEnglishSkills");

                    b.HasIndex("ApplicationUserIdExpertHardSkills");

                    b.HasIndex("ApplicationUserIdExpertSoftSkills");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CurrentGradeId");

                    b.HasIndex("NextGradeId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("HRTech.Domain.ExcelFileUsers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("Content")
                        .HasColumnType("longblob");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FileGuid")
                        .HasColumnType("char(36)");

                    b.Property<string>("FileName")
                        .HasColumnType("longtext");

                    b.Property<string>("FileType")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ExcelFileUsers");
                });

            modelBuilder.Entity("HRTech.Domain.FileTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("Content")
                        .HasColumnType("longblob");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FileName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("FileTemplates");
                });

            modelBuilder.Entity("HRTech.Domain.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("HRTech.Domain.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("Content")
                        .HasColumnType("longblob");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FileGuid")
                        .HasColumnType("char(36)");

                    b.Property<string>("FileName")
                        .HasColumnType("longtext");

                    b.Property<string>("FileType")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("HRTech.Domain.PersonalDevelopmentPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("varchar(255)");

                    b.Property<byte[]>("Content")
                        .HasColumnType("longblob");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FileGuid")
                        .HasColumnType("char(36)");

                    b.Property<string>("FileName")
                        .HasColumnType("longtext");

                    b.Property<string>("FileType")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("PersonalDevelopmentPlans");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HRTech.Domain.ApplicationUser", b =>
                {
                    b.HasOne("HRTech.Domain.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId");

                    b.HasOne("HRTech.Domain.Grade", "Grades")
                        .WithOne("ApplicationUser")
                        .HasForeignKey("HRTech.Domain.ApplicationUser", "GradeId");

                    b.Navigation("Company");

                    b.Navigation("Grades");
                });

            modelBuilder.Entity("HRTech.Domain.Comment", b =>
                {
                    b.HasOne("HRTech.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany("Comments")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("HRTech.Domain.Evaluation", "Evaluation")
                        .WithMany("Comments")
                        .HasForeignKey("EvaluationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Evaluation");
                });

            modelBuilder.Entity("HRTech.Domain.Company", b =>
                {
                    b.HasOne("HRTech.Domain.Address", "Address")
                        .WithMany("Companies")
                        .HasForeignKey("AddressId");

                    b.HasOne("HRTech.Domain.ExcelFileUsers", "ExcelFileUsers")
                        .WithMany("Company")
                        .HasForeignKey("ExcelFileUsersId");

                    b.Navigation("Address");

                    b.Navigation("ExcelFileUsers");
                });

            modelBuilder.Entity("HRTech.Domain.Evaluation", b =>
                {
                    b.HasOne("HRTech.Domain.ApplicationUser", "ApplicationUsers")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("HRTech.Domain.ApplicationUser", "ApplicationUserExpertEnglishSkills")
                        .WithMany()
                        .HasForeignKey("ApplicationUserIdExpertEnglishSkills");

                    b.HasOne("HRTech.Domain.ApplicationUser", "ApplicationUserExpertHardSkills")
                        .WithMany()
                        .HasForeignKey("ApplicationUserIdExpertHardSkills");

                    b.HasOne("HRTech.Domain.ApplicationUser", "ApplicationUserExpertSoftSkills")
                        .WithMany()
                        .HasForeignKey("ApplicationUserIdExpertSoftSkills");

                    b.HasOne("HRTech.Domain.Company", "Company")
                        .WithMany("Evaluations")
                        .HasForeignKey("CompanyId");

                    b.HasOne("HRTech.Domain.Grade", "CurrentGrade")
                        .WithMany()
                        .HasForeignKey("CurrentGradeId");

                    b.HasOne("HRTech.Domain.Grade", "NextGrade")
                        .WithMany()
                        .HasForeignKey("NextGradeId");

                    b.Navigation("ApplicationUserExpertEnglishSkills");

                    b.Navigation("ApplicationUserExpertHardSkills");

                    b.Navigation("ApplicationUserExpertSoftSkills");

                    b.Navigation("ApplicationUsers");

                    b.Navigation("Company");

                    b.Navigation("CurrentGrade");

                    b.Navigation("NextGrade");
                });

            modelBuilder.Entity("HRTech.Domain.Grade", b =>
                {
                    b.HasOne("HRTech.Domain.Company", "Company")
                        .WithMany("GradesCollection")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("HRTech.Domain.Image", b =>
                {
                    b.HasOne("HRTech.Domain.Company", "Company")
                        .WithOne("Image")
                        .HasForeignKey("HRTech.Domain.Image", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("HRTech.Domain.PersonalDevelopmentPlan", b =>
                {
                    b.HasOne("HRTech.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany("PersonalDevelopmentPlans")
                        .HasForeignKey("ApplicationUserId");

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HRTech.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HRTech.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRTech.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HRTech.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HRTech.Domain.Address", b =>
                {
                    b.Navigation("Companies");
                });

            modelBuilder.Entity("HRTech.Domain.ApplicationUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PersonalDevelopmentPlans");
                });

            modelBuilder.Entity("HRTech.Domain.Company", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Evaluations");

                    b.Navigation("GradesCollection");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("HRTech.Domain.Evaluation", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("HRTech.Domain.ExcelFileUsers", b =>
                {
                    b.Navigation("Company");
                });

            modelBuilder.Entity("HRTech.Domain.Grade", b =>
                {
                    b.Navigation("ApplicationUser");
                });
#pragma warning restore 612, 618
        }
    }
}
