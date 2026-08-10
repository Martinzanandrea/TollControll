namespace TollControl.Api.Models;

public class TollTransaction
{
    public int Id { get; set; }
    public int TagId { get; set; }
    public int StationId { get; set; }
    public int LaneId { get; set; }
    public int TariffId { get; set; }
    public decimal Amount { get; set; }
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public string Status { get; set; } = null!;          // APROBADO, RECHAZADO
    public string? RejectionReason { get; set; }
    public DateTimeOffset OccurredAt { get; set; } = DateTimeOffset.UtcNow;

    public Tag Tag { get; set; } = null!;
    public Station Station { get; set; } = null!;
    public Lane Lane { get; set; } = null!;
    public Tariff Tariff { get; set; } = null!;
    public BalanceMovement? BalanceMovement { get; set; } // 1:0..1
}
