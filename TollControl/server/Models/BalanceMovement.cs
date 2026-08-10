namespace TollControl.Api.Models;

public class BalanceMovement
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int? TollTransactionId { get; set; }          // NULL salvo type = COBRO_PEAJE
    public string Type { get; set; } = null!;             // RECARGA, COBRO_PEAJE, AJUSTE, REVERSO
    public decimal Amount { get; set; }
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public DateTimeOffset OccurredAt { get; set; } = DateTimeOffset.UtcNow;

    public Account Account { get; set; } = null!;
    public TollTransaction? TollTransaction { get; set; }
}
