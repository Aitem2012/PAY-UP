using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PAY_UP.Application.Abstracts.Infrastructure;
using PAY_UP.Application.Abstracts.Persistence;
using PAY_UP.Domain.AppUsers;
using PAY_UP.Infrastructure.Email;
using PAY_UP.Infrastructure.Sms;
using PAY_UP.Persistence.Context;

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
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<IEmailService, EmailService>();
        }

    }
}
