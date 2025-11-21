using APImovil3.Models;

namespace APImovil3.Repositories;

/// <summary>
/// Interfaz para el repositorio de Usuarios
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Obtiene todos los usuarios con su rol
    /// </summary>
    Task<IEnumerable<User>> GetAllAsync();

    /// <summary>
    /// Obtiene un usuario por su ID con su rol
    /// </summary>
    Task<User?> GetByIdAsync(int id);

    /// <summary>
    /// Obtiene un usuario por su email
    /// </summary>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Obtiene usuarios filtrados por rol
    /// </summary>
    Task<IEnumerable<User>> GetByRoleIdAsync(int roleId);

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    Task<User> CreateAsync(User user);

    /// <summary>
    /// Actualiza un usuario existente
    /// </summary>
    Task<User> UpdateAsync(User user);

    /// <summary>
    /// Elimina un usuario
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Verifica si existe un usuario con el email especificado
    /// </summary>
    Task<bool> EmailExistsAsync(string email);

    /// <summary>
    /// Verifica si existe un usuario con el ID especificado
    /// </summary>
    Task<bool> ExistsAsync(int id);
}

