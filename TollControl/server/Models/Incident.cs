namespace TollControl.Api.Models;

public class Incident
{
    public int Id { get; set; }
    public int? StationId { get; set; }
    public int? LaneId { get; set; }
    public string Type { get; set; } = null!;              // TAG_NO_DETECTADO, VIA_FUERA_DE_SERVICIO...
    public string? Description { get; set; }
    public string Status { get; set; } = "ABIERTA";
    public DateTimeOffset OccurredAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ResolvedAt { get; set; }

    public Station? Station { get; set; }
    public Lane? Lane { get; set; }
}
