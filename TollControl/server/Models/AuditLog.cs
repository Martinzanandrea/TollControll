namespace TollControl.Api.Models;

public class AuditLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Action { get; set; } = null!;           // ej: MODIFICAR_TARIFA
    public string Entity { get; set; } = null!;            // ej: Tariff
    public int? EntityId { get; set; }
    public string? OldValue { get; set; }                  // JSONB
    public string? NewValue { get; set; }                  // JSONB
    public DateTimeOffset OccurredAt { get; set; } = DateTimeOffset.UtcNow;

    public User User { get; set; } = null!;
}
