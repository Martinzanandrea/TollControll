namespace TollControl.Api.Models;

public class Tag
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int VehicleId { get; set; }
    public string SerialCode { get; set; } = null!;
    public string Status { get; set; } = "ACTIVO";       // ACTIVO, INACTIVO, BLOQUEADO, PERDIDO
    public DateTimeOffset IssuedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeactivatedAt { get; set; }

    public Account Account { get; set; } = null!;
    public Vehicle Vehicle { get; set; } = null!;
    public ICollection<TollTransaction> TollTransactions { get; set; } = new List<TollTransaction>();
}
