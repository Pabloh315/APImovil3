using Microsoft.EntityFrameworkCore;
using APImovil3.Data;
using APImovil3.Models;

namespace APImovil3.Repositories;

/// <summary>
/// Implementación del repositorio de Usuarios
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructor que recibe el contexto de base de datos
    /// </summary>
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los usuarios con su rol
    /// </summary>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Role)
            .OrderBy(u => u.FullName)
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene un usuario por su ID con su rol
    /// </summary>
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }

    /// <summary>
    /// Obtiene un usuario por su email
    /// </summary>
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Obtiene usuarios filtrados por rol
    /// </summary>
    public async Task<IEnumerable<User>> GetByRoleIdAsync(int roleId)
    {
        return await _context.Users
            .Include(u => u.Role)
            .Where(u => u.RoleId == roleId)
            .OrderBy(u => u.FullName)
            .ToListAsync();
    }

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    /// <summary>
    /// Actualiza un usuario existente
    /// </summary>
    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    /// <summary>
    /// Elimina un usuario
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Verifica si existe un usuario con el email especificado
    /// </summary>
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    /// <summary>
    /// Verifica si existe un usuario con el ID especificado
    /// </summary>
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Users.AnyAsync(u => u.UserId == id);
    }
}

