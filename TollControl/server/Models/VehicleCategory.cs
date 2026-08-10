namespace TollControl.Api.Models;

public class VehicleCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;           // AUTO, CAMIONETA, CAMION, MOTO...
    public string? Description { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public ICollection<Tariff> Tariffs { get; set; } = new List<Tariff>();
}
