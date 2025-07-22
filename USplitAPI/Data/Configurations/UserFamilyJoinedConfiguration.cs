using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USplitAPI.Domain;

namespace USplitAPI.Data.Configurations;

public class UserFamilyJoinedConfiguration : IEntityTypeConfiguration<UserFamilyJoinedEntity>
{
    public void Configure(EntityTypeBuilder<UserFamilyJoinedEntity> builder)
    {
        builder.HasKey(uf => new { uf.UserId, uf.FamilyId });

        builder
            .HasOne(uf => uf.User)
            .WithMany(u => u.UserFamilyList)
            .HasForeignKey(uf => uf.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(uf => uf.Family)
            .WithMany(f => f.UserFamilyList)
            .HasForeignKey(uf => uf.FamilyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(uf => uf.Debts)
            .WithOne(d => d.UserFamily)
            .HasForeignKey(d => new { UserId = d.OwnerUserId, FamilyId = d.OwnerFamilyId })
            .OnDelete(DeleteBehavior.Cascade);
    }
}