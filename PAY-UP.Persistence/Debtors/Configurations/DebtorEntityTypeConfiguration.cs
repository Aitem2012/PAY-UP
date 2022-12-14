using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PAY_UP.Domain.Debtors;

namespace PAY_UP.Persistence.Debtors.Configuration{
    public class DebtorEntityTypeConfiguration : IEntityTypeConfiguration<Debtor>
    {
        public void Configure(EntityTypeBuilder<Debtor> builder)
        {
            builder.ToTable("Debtors");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AmountOwed)
                .HasColumnType("money").HasPrecision(2);
            builder.Property(x => x.AmountPaid)
                .HasColumnType("money").HasPrecision(2);
        }
    }
}