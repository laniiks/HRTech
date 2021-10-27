using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Common.HtmlMessage;
using HRTech.Application.Abstractions;
using HRTech.Application.Mapping;
using HRTech.Application.Services.Address.Implementations;
using HRTech.Application.Services.Address.Interfaces;
using HRTech.Application.Services.Company.Implementations;
using HRTech.Application.Services.Company.Interfaces;
using HRTech.Application.Services.CompanyExelFileUsers.Implementations;
using HRTech.Application.Services.CompanyExelFileUsers.Interfaces;
using HRTech.Application.Services.Mail.Interfaces;
using HRTech.Application.Services.User.Implementations;
using HRTech.Application.Services.User.Interfaces;
using HRTech.Domain;
using HRTech.Infrastructure.Consumers;
using HRTech.Infrastructure.DataAccess;
using HRTech.Infrastructure.DataAccess.Repositories;
using HRTech.Infrastructure.GeneratePassword;
using HRTech.Infrastructure.Mail;
using HRTech.Infrastructure.UsersFromExcelFile;
using HRTech.WebApi.Mapping;
using HRTech.WebApi.Utils;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HRTech.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(conf =>
            {
                conf.AddConsumer<SendEmailConsumer>();

                conf.UsingRabbitMq((context, c) =>
                {
                    c.Host("localhost", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    c.ReceiveEndpoint("send_email", e => e.ConfigureConsumer<SendEmailConsumer>(context));
                });
            });

            services.AddMassTransitHostedService();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "HRTech.WebApi", Version = "v1"});
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                        Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n
                        Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
            
            InstallAuthentication(services);
            DatabaseContextInstaller.ConfigureDbContext(services);

            services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()))
                //Services
                .AddTransient<IUserService, UserService>()
                .AddTransient<ICompanyService, CompanyService>()
                .AddTransient<IAddressService, AddressService>()
                .AddTransient<ICompanyExcelFileUsers, CompanyExcelFileUsersService>()
                
                //Repositories
                .AddTransient<ICompanyRepository, CompanyRepository>()
                .AddTransient<IRepository<Image>, BaseRepository<Image>>()
                .AddTransient<IRepository<ExcelFileUsers>, BaseRepository<ExcelFileUsers>>()
                .AddTransient<IRepository<Address>, BaseRepository<Address>>()
                
                //Infrastructure
                .AddTransient<IGetUsersFromExcelFile, GetUsersFromExcelFile>()
                .AddTransient<IGeneratePassword, GeneratePassword>()
                .Configure<MailSettings>(Configuration.GetSection("MailSettings"))
                .AddTransient<IMailService, MailService>()
                .AddTransient<HtmlMessage>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HRTech.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            IdentityDataInitializer.SeedData(userManager, roleManager);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        private void InstallAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Token:Issuer"],
                        ValidAudience = Configuration["Token:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"]))
                    };
                });
        }
        
        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationUserMapProfile>();
                cfg.AddProfile<ApiMappingProfile>();
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<CompanyProfile>();
                cfg.AddProfile<LogoProfile>();
                cfg.AddProfile<AddressProfile>();
                cfg.AddProfile<ExcelFileUsersProfile>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}