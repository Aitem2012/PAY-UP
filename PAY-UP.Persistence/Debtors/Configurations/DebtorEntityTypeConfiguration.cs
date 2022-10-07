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
        }
    }
}