using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using USplitAPI.Domain;

namespace USplitAPI.Data.Configurations;

public class FamilyConfiguration : IEntityTypeConfiguration<FamilyEntity>
{
    public void Configure(EntityTypeBuilder<FamilyEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityAlwaysColumn();

        builder
            .HasOne<UserEntity>(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.OwnerUserId);
    }
}