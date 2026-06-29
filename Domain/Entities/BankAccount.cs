namespace Domain.Entities;

public class BankAccount
{
    public Guid Id { get; set; }

    public string Alias { get; set; } = string.Empty;

    public string BankName { get; set; } = string.Empty;

    public string AccountNumber { get; set; } = string.Empty;

    public string Holder { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public Guid UserId { get; set; }

    // Propiedad de navegación
    public User User { get; set; } = null!;
}