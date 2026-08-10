namespace TollControl.Api.Models;

public class Account
{
    public int Id { get; set; }
    public int CustomerId { get; set; }                 // FK única -> Customer (1:1)
    public decimal Balance { get; set; } = 0;
    public string Status { get; set; } = "ACTIVA";       // ACTIVA, SUSPENDIDA, CERRADA
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Customer Customer { get; set; } = null!;
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<BalanceMovement> BalanceMovements { get; set; } = new List<BalanceMovement>();
}
