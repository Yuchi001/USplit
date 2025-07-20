using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USplitAPI.Domain;

namespace USplitAPI.Data.Configurations;

public class CurrentBalanceConfiguration : IEntityTypeConfiguration<CurrentBalanceEntity>
{
    public void Configure(EntityTypeBuilder<CurrentBalanceEntity> builder)
    {
        builder
            .HasOne(cb => cb.Family)
            .WithMany(f => f.CurrentBalanceList)
            .HasForeignKey(cb => cb.FamilyId);
        
        builder
            .HasOne(cb => cb.User)
            .WithMany(u => u.CurrentBalanceList)
            .HasForeignKey(cb => cb.UserId);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityAlwaysColumn();
    }
}