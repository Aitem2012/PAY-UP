using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

namespace PAY_UP.Persistence.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddIdentityCore<AppUser>();
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<JWTData>(config.GetSection(JWTData.Data));
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
            services.AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssembly(typeof(SmSDtoValidator).GetTypeInfo().Assembly);
            });
        }

    }
}
