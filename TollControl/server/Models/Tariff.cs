namespace TollControl.Api.Models;

public class Tariff
{
    public int Id { get; set; }
    public int StationId { get; set; }
    public int VehicleCategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset ValidFrom { get; set; }
    public DateTimeOffset? ValidTo { get; set; }         // NULL = vigente
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public Station Station { get; set; } = null!;
    public VehicleCategory VehicleCategory { get; set; } = null!;
    public ICollection<TollTransaction> TollTransactions { get; set; } = new List<TollTransaction>();
}
