using APImovil3.Models;

namespace APImovil3.Repositories;

/// <summary>
/// Interfaz para el repositorio de Roles
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Obtiene todos los roles
    /// </summary>
    Task<IEnumerable<Role>> GetAllAsync();

    /// <summary>
    /// Obtiene un rol por su ID
    /// </summary>
    Task<Role?> GetByIdAsync(int id);

    /// <summary>
    /// Obtiene un rol por su nombre
    /// </summary>
    Task<Role?> GetByNameAsync(string name);

    /// <summary>
    /// Crea un nuevo rol
    /// </summary>
    Task<Role> CreateAsync(Role role);

    /// <summary>
    /// Actualiza un rol existente
    /// </summary>
    Task<Role> UpdateAsync(Role role);

    /// <summary>
    /// Elimina un rol
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Verifica si existe un rol con el ID especificado
    /// </summary>
    Task<bool> ExistsAsync(int id);
}

