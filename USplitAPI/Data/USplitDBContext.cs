using Microsoft.EntityFrameworkCore;
using USplitAPI.Domain;

namespace USplitAPI.Data;

public class USplitDBContext : DbContext
{
    public DbSet<TransactionEntity> Transactions { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<FamilyEntity> Families { get; set; }
    public DbSet<CurrentBalanceEntity> BalanceList { get; set; }
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

    public USplitDBContext(DbContextOptions<USplitDBContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(USplitDBContext).Assembly);
}