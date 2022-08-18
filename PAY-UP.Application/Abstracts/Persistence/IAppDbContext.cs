using Microsoft.EntityFrameworkCore;
using PAY_UP.Domain.AppUsers;
using PAY_UP.Domain.Mailing;
using PAY_UP.Domain.Messaging;

namespace PAY_UP.Application.Abstracts.Persistence
{
    public interface IAppDbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Sms> Sms { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
