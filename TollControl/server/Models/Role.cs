namespace TollControl.Api.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;         // ADMIN, OPERADOR, CLIENTE
    public string? Description { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}
