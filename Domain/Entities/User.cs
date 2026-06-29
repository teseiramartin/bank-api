using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public RoleEnum Role { get; set; }

    // Propiedad de navegación
    public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
}