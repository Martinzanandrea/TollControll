namespace TollControl.Api.Models;

public class Lane
{
    public int Id { get; set; }
    public int StationId { get; set; }
    public int Number { get; set; }
    public string Status { get; set; } = "HABILITADA";   // HABILITADA, FUERA_DE_SERVICIO

    public Station Station { get; set; } = null!;
    public ICollection<TollTransaction> TollTransactions { get; set; } = new List<TollTransaction>();
    public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}
