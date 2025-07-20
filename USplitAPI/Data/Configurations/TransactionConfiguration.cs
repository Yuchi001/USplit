using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USplitAPI.Domain;

namespace USplitAPI.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder
            .HasOne(t => t.User)
            .WithMany(u => u.TransactionList)
            .HasForeignKey(t => t.UserId);

        builder
            .HasOne(t => t.Family)
            .WithMany(f => f.TransactionList)
            .HasForeignKey(t => t.FamilyId);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityAlwaysColumn();
    }
}