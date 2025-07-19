using Microsoft.EntityFrameworkCore;
using USplitAPI.Domain;

namespace USplitAPI.Data;

public class USplitDBContext : DbContext
{
    public DbSet<TransactionEntity> Transactions { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<FamilyEntity> Families { get; set; }
    
    public USplitDBContext(DbContextOptions<USplitDBContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserFamilyJoinedEntity>()
            .HasKey(uf => new { uf.UserId, uf.FamilyId });

        modelBuilder.Entity<UserFamilyJoinedEntity>()
            .HasOne(uf => uf.User)
            .WithMany(u => u.UserFamilyList)
            .HasForeignKey(uf => uf.UserId);

        modelBuilder.Entity<UserFamilyJoinedEntity>()
            .HasOne(uf => uf.Family)
            .WithMany(f => f.UserFamilyList)
            .HasForeignKey(uf => uf.FamilyId);

        modelBuilder.Entity<TransactionEntity>()
            .HasOne(t => t.User)
            .WithMany(u => u.TransactionList)
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<TransactionEntity>()
            .HasOne(t => t.Family)
            .WithMany(f => f.TransactionList)
            .HasForeignKey(t => t.FamilyId);
    }
}