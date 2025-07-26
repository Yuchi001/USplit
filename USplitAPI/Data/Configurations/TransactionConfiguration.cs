using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USplitAPI.Domain;

namespace USplitAPI.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder
            .HasMany(t => t.Debts)
            .WithOne(d => d.Transaction)
            .HasForeignKey(d => d.TransactionId);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityAlwaysColumn();
    }
}