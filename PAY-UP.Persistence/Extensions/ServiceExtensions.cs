using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PAY_UP.Application;
using PAY_UP.Application.Abstracts.Infrastructure;
using PAY_UP.Application.Abstracts.Persistence;
using PAY_UP.Application.Abstracts.Repositories;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos.Token;
using PAY_UP.Application.Services;
using PAY_UP.Application.Validators.SmS;
using PAY_UP.Domain.AppUsers;
using PAY_UP.Infrastructure.Email;
using PAY_UP.Infrastructure.Sms;
using PAY_UP.Infrastructure.Token;
using PAY_UP.Persistence.Context;
using PAY_UP.Persistence.Repositories;
using System.Reflection;
using System.Text;

namespace PAY_UP.Persistence.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddIdentityCore<AppUser>();
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<JWTData>(config.GetSection(JWTData.Data));

            services.AddHangfire(option => option
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }
            ));
            services.AddHangfireServer(option =>
            {
                option.SchedulePollingInterval = TimeSpan.FromMinutes(5);
            });
        }

        public static void AddAuthenticationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = config.GetSection("JWTConfigurations:Issuer").Value,
                  ValidAudience = config.GetSection("JWTConfigurations:Audience").Value,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWTConfigurations:SecretKey").Value))
              };
          })
          .AddCookie(options =>
          {
              options.ForwardAuthenticate = CookieAuthenticationDefaults.AuthenticationScheme;
          });
          services.AddAuthorization(options =>{
            options.AddPolicy("AdminOnly", policy => policy.RequireClaim("admin"));
          });
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PayUpMappingProfile));
            services.AddHttpContextAccessor();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICreditorRepository, CreditorRepository>();
            services.AddScoped<ICreditorService, CreditorService>();
            services.AddScoped<ISchedulingService, SchedulingService>();
            services.AddScoped<IDebitorService, DebitorService>();
            services.AddScoped<IDebitorRepository, DebitorRepository>();
            services.AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssembly(typeof(SmSDtoValidator).GetTypeInfo().Assembly);
            });

        }

    }
}
