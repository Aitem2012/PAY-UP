using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAY_UP.Domain.AppUsers;

namespace PAY_UP.Persistence.AppUsers.Configurations
{
    public class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Smses)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.AppUserId);

            builder.HasMany(x => x.Mails)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.AppUserId);
            
            builder.HasMany(x => x.Debtors)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.AppUserId);
            
            builder.HasMany(x => x.Creditors)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.AppUserId);
        }
    }
}
