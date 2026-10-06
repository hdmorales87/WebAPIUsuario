using Microsoft.EntityFrameworkCore;
using WebAPIUsuario.Data;
using WebAPIUsuario.Models;

namespace WebAPIUsuario.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario?> UpdateAsync(Usuario usuario)
    {
        var usuarioExistente = await _context.Usuarios.FindAsync(usuario.Id);
        if (usuarioExistente == null)
        {
            return null;
        }

        _context.Entry(usuarioExistente).CurrentValues.SetValues(usuario);
        await _context.SaveChangesAsync();
        return usuarioExistente;
    }

    public async Task<bool> DeleteAsync(int id)
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

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Usuarios.AnyAsync(u => u.Id == id);
    }
}
