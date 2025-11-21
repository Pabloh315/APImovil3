namespace APImovil3.Models;

/// <summary>
/// Modelo que representa un Rol en el sistema
/// </summary>
public class Role
{
    /// <summary>
    /// Identificador único del rol
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Nombre del rol (máximo 100 caracteres)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del rol (máximo 200 caracteres, opcional)
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Navegación: Lista de usuarios que tienen este rol
    /// </summary>
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

