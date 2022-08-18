using Microsoft.EntityFrameworkCore;

namespace PAY_UP.Persistence.Sms.Configurations
{
    public class SmsEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Messaging.Sms>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Messaging.Sms> builder)
        {
            builder.ToTable("Smses");
            builder.HasKey(x => x.Id);
        }
    }
}
