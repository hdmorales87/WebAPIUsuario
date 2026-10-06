using Microsoft.EntityFrameworkCore;
using WebAPIUsuario.Data;
using WebAPIUsuario.DTOs;
using WebAPIUsuario.Models;

namespace WebAPIUsuario.Services;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UsuarioResponseDto>> GetAllUsuariosAsync()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return usuarios.Select(MapToDto);
    }

    public async Task<UsuarioResponseDto?> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
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

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return MapToDto(usuario);
    }

    public async Task<UsuarioResponseDto?> UpdateUsuarioAsync(int id, UpdateUsuarioDto usuarioDto)
    {
        var usuarioExistente = await _context.Usuarios.FindAsync(id);
        if (usuarioExistente == null)
        {
            return null;
        }

        usuarioExistente.Nombres = usuarioDto.Nombres;
        usuarioExistente.Apellidos = usuarioDto.Apellidos;
        usuarioExistente.Correo = usuarioDto.Correo;
        usuarioExistente.Username = usuarioDto.Username;

        await _context.SaveChangesAsync();
        return MapToDto(usuarioExistente);
    }

    public async Task<bool> DeleteUsuarioAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
        {
            return false;
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
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
