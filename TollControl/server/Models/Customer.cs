namespace TollControl.Api.Models;

public class Customer
{
    public int Id { get; set; }
    public int UserId { get; set; }                    // FK única -> User (0..1 desde User)
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string DocumentId { get; set; } = null!;     // DNI/CUIT
    public string? Phone { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public User User { get; set; } = null!;
    public Account? Account { get; set; }               // 1:1
}
