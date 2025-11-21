using Microsoft.EntityFrameworkCore;
using APImovil3.Data;
using APImovil3.Models;

namespace APImovil3.Repositories;

/// <summary>
/// Implementación del repositorio de Roles
/// </summary>
public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor que recibe el contexto de base de datos
    /// </summary>
    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los roles
    /// </summary>
    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles
            .OrderBy(r => r.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene un rol por su ID
    /// </summary>
    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.RoleId == id);
    }

    /// <summary>
    /// Obtiene un rol por su nombre
    /// </summary>
    public async Task<Role?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == name);
    }

    /// <summary>
    /// Crea un nuevo rol
    /// </summary>
    public async Task<Role> CreateAsync(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    /// <summary>
    /// Actualiza un rol existente
    /// </summary>
    public async Task<Role> UpdateAsync(Role role)
    {
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
        return role;
    }

    /// <summary>
    /// Elimina un rol
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var role = await GetByIdAsync(id);
        if (role == null)
            return false;

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Verifica si existe un rol con el ID especificado
    /// </summary>
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Roles.AnyAsync(r => r.RoleId == id);
    }
}

