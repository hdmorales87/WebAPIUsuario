using WebAPIUsuario.DTOs;

namespace WebAPIUsuario.Services;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioResponseDto>> GetAllUsuariosAsync();
    Task<UsuarioResponseDto?> GetUsuarioByIdAsync(int id);
    Task<UsuarioResponseDto> CreateUsuarioAsync(CreateUsuarioDto usuarioDto);
    Task<UsuarioResponseDto?> UpdateUsuarioAsync(int id, UpdateUsuarioDto usuarioDto);
    Task<bool> DeleteUsuarioAsync(int id);
}
