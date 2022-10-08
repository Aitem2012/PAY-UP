using Microsoft.EntityFrameworkCore;
using PAY_UP.Domain.Creditors;

namespace PAY_UP.Persistence.Creditors.Configuration{
    public class CreditorEntityTypeConfiguration : IEntityTypeConfiguration<Creditor>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Creditor> builder)
        {
            builder.ToTable("Creditors");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AmountOwed)
                .HasColumnType("money");
            builder.Property(x => x.AmountPaid)
                .HasColumnType("money");
        }
    }
}