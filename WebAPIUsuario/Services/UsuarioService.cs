using WebAPIUsuario.DTOs;
using WebAPIUsuario.Models;
using WebAPIUsuario.Repositories;

namespace WebAPIUsuario.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> GetAllUsuariosAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return usuarios.Select(MapToDto);
    }

    public async Task<UsuarioResponseDto?> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        return usuario != null ? MapToDto(usuario) : null;
    }

    public async Task<UsuarioResponseDto> CreateUsuarioAsync(CreateUsuarioDto usuarioDto)
    {
        var usuario = new Usuario
        {
            Nombres = usuarioDto.Nombres,
            Apellidos = usuarioDto.Apellidos,
            Correo = usuarioDto.Correo,
            Username = usuarioDto.Username,
            FechaCreacion = DateTime.Now
        };

        var usuarioCreado = await _usuarioRepository.AddAsync(usuario);
        return MapToDto(usuarioCreado);
    }

    public async Task<UsuarioResponseDto?> UpdateUsuarioAsync(int id, UpdateUsuarioDto usuarioDto)
    {
        var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
        if (usuarioExistente == null)
        {
            return null;
        }

        usuarioExistente.Nombres = usuarioDto.Nombres;
        usuarioExistente.Apellidos = usuarioDto.Apellidos;
        usuarioExistente.Correo = usuarioDto.Correo;
        usuarioExistente.Username = usuarioDto.Username;

        var usuarioActualizado = await _usuarioRepository.UpdateAsync(usuarioExistente);
        return usuarioActualizado != null ? MapToDto(usuarioActualizado) : null;
    }

    public async Task<bool> DeleteUsuarioAsync(int id)
    {
        return await _usuarioRepository.DeleteAsync(id);
    }

    private static UsuarioResponseDto MapToDto(Usuario usuario)
    {
        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nombres = usuario.Nombres,
            Apellidos = usuario.Apellidos,
            Correo = usuario.Correo,
            Username = usuario.Username,
            FechaCreacion = usuario.FechaCreacion
        };
    }
}
