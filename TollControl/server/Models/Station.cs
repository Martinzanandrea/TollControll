namespace TollControl.Api.Models;

public class Station
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Location { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<Lane> Lanes { get; set; } = new List<Lane>();
    public ICollection<Tariff> Tariffs { get; set; } = new List<Tariff>();
    public ICollection<TollTransaction> TollTransactions { get; set; } = new List<TollTransaction>();
    public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}
