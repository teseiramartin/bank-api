using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class BankApiDbContext : DbContext
{
    public BankApiDbContext(DbContextOptions<BankApiDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankApiDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}