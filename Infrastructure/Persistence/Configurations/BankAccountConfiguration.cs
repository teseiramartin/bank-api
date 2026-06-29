using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Alias)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.BankName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.AccountNumber)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.Holder)
            .HasMaxLength(100);

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Balance)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.User)
            .WithMany(x => x.BankAccounts)
            .HasForeignKey(x => x.UserId);

        /*// Configuración de la relación con User
        builder.HasOne<User>()
            .WithMany(u => u.BankAccounts)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);*/
    }
}