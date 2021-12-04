using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Common.HtmlMessage;
using HRTech.Application.Abstractions;
using HRTech.Application.Mapping;
using HRTech.Application.Services.Address.Implementations;
using HRTech.Application.Services.Address.Interfaces;
using HRTech.Application.Services.Comment.Implementations;
using HRTech.Application.Services.Comment.Interfaces;
using HRTech.Application.Services.Company.Implementations;
using HRTech.Application.Services.Company.Interfaces;
using HRTech.Application.Services.CompanyExelFileUsers.Implementations;
using HRTech.Application.Services.CompanyExelFileUsers.Interfaces;
using HRTech.Application.Services.Evaluation.Implementations;
using HRTech.Application.Services.Evaluation.Interfaces;
using HRTech.Application.Services.Grade.Implementations;
using HRTech.Application.Services.Grade.Interfaces;
using HRTech.Application.Services.Mail.Interfaces;
using HRTech.Application.Services.PDP.Implementations;
using HRTech.Application.Services.PDP.Interfaces;
using HRTech.Application.Services.TemplateFile.Implementations;
using HRTech.Application.Services.TemplateFile.Interfaces;
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
using Microsoft.EntityFrameworkCore;
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.AddMassTransit(conf =>
            {
                conf.AddConsumer<SendEmailConsumer>();

                conf.UsingRabbitMq((context, c) =>
                {
                    c.Host("78.24.219.180", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });
                    // c.Host("localhost", host =>
                    // {
                    //     host.Username("guest");
                    //     host.Password("guest");
                    // });

                    c.ReceiveEndpoint("send_email", e => e.ConfigureConsumer<SendEmailConsumer>(context));
                });
            });

            services.AddMassTransitHostedService();
            
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
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
                .AddTransient<IPersonalDeveloperPlanService, PersonalDeveloperPlanService>()
                .AddTransient<ITemplateFileService, TemplateFileService>()
                .AddTransient<IGradeService, GradeService>()
                .AddTransient<IEvaluationService, EvaluationService>()
                .AddTransient<ICommentService, CommentService>()

                //Repositories
                .AddTransient<ICompanyRepository, CompanyRepository>()
                .AddTransient<IPersonalDevelopmentPlanRepository, PersonalDevelopmentPlanRepository>()
                .AddTransient<IRepository<Image>, BaseRepository<Image>>()
                .AddTransient<IRepository<ExcelFileUsers>, BaseRepository<ExcelFileUsers>>()
                .AddTransient<IRepository<Address>, BaseRepository<Address>>()
                .AddTransient<IGradeRepository, GradeRepository>()
                .AddTransient<IRepository<FileTemplate>, BaseRepository<FileTemplate>>()
                .AddTransient<IEvaluationRepository, EvaluationRepository>()
                .AddTransient<IApplicationUserRepository, ApplicationUserRepository>()
                .AddTransient<IRepository<Comment>, BaseRepository<Comment>>()

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
            //Init migrations
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            db.Database.Migrate();
            
            app.UseCors("CorsPolicy");

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
                cfg.AddProfile<PersonalDevelopmentPlanProfile>();
                cfg.AddProfile<GradeProfile>();
                cfg.AddProfile<FileTemplateProfile>();
                cfg.AddProfile<CommentProfile>();
                //cfg.AddProfile<EvaluationProfile>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}