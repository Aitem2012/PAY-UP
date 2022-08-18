using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAY_UP.Domain.Mailing;

namespace PAY_UP.Persistence.Mailing.Configurations
{
    public class MailEntityTypeConfiguration : IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder.ToTable("Mails");
            builder.HasKey(x => x.Id);
        }
    }
}
