using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(x => x.Email)
            .IsUnique();
        
        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Role)
            .IsRequired();

        /*// Configuración de la relación con BankAccount
        builder.HasMany(u => u.BankAccounts)
            .WithOne()
            .HasForeignKey("UserId") // Clave foránea en BankAccount
            .OnDelete(DeleteBehavior.Cascade); // Comportamiento de eliminación en cascada*/
    }
}