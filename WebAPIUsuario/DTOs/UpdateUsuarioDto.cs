using System.ComponentModel.DataAnnotations;

namespace WebAPIUsuario.DTOs;

public class UpdateUsuarioDto
{
    [Required]
    [MaxLength(50)]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Apellidos { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Correo { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Username { get; set; }
}
