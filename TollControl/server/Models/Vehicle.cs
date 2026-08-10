namespace TollControl.Api.Models;

public class Vehicle
{
    public int Id { get; set; }
    public int VehicleCategoryId { get; set; }
    public string LicensePlate { get; set; } = null!;
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public VehicleCategory VehicleCategory { get; set; } = null!;
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
