namespace WebAPIUsuario.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string? Username { get; set; }
    public DateTime? FechaCreacion { get; set; }
}
