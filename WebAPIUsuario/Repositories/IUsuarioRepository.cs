using WebAPIUsuario.Models;

namespace WebAPIUsuario.Repositories;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario> AddAsync(Usuario usuario);
    Task<Usuario?> UpdateAsync(Usuario usuario);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
