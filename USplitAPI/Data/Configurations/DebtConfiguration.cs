using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USplitAPI.Domain;

namespace USplitAPI.Data.Configurations;

public class DebtConfiguration : IEntityTypeConfiguration<DebtEntity>
{
    public void Configure(EntityTypeBuilder<DebtEntity> builder)
    {
        builder
            .HasOne(d => d.LenderUser)
            .WithMany()
            .HasForeignKey(d => d.LenderUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}