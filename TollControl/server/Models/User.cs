namespace TollControl.Api.Models;

public class User
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Role Role { get; set; } = null!;
    public Customer? Customer { get; set; }            // 0..1
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
