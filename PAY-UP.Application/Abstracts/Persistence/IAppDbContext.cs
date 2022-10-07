using Microsoft.EntityFrameworkCore;
using PAY_UP.Domain.AppUsers;
using PAY_UP.Domain.Creditors;
using PAY_UP.Domain.Debtors;
using PAY_UP.Domain.Mailing;
using PAY_UP.Domain.Messaging;

namespace PAY_UP.Application.Abstracts.Persistence
{
    public interface IAppDbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Sms> Sms { get; set; }
        public DbSet<Creditor> Creditors { get; set; }
        public DbSet<Debtor> Debtors { get; set; }        
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
