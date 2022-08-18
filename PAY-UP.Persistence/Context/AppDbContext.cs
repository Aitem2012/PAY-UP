using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PAY_UP.Application.Abstracts.Persistence;
using PAY_UP.Domain.AppUsers;
using PAY_UP.Domain.Common;
using PAY_UP.Domain.Mailing;
using System.Reflection;

namespace PAY_UP.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>, IAppDbContext
    {

        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        // ToDo: Add Current User Service Here
                        //entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.DateCreated = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.DateUpdated = DateTime.Now;
                        break;
                }
            }

            await DispatchEvents();

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        private async Task DispatchEvents()
        {
            var domainEventEntities = ChangeTracker.Entries<BaseEntity>().Select(x => x.Entity);
        }

        DbSet<AppUser> IAppDbContext.Users { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Domain.Messaging.Sms> Sms { get; set; }

    }
}
