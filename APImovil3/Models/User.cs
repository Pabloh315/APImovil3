namespace APImovil3.Models;

/// <summary>
/// Modelo que representa un Usuario en el sistema
/// </summary>
public class User
{
    /// <summary>
    /// Identificador único del usuario
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Nombre completo del usuario (máximo 150 caracteres)
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Email del usuario (máximo 150 caracteres, único)
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Hash de la contraseña del usuario
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Identificador del rol asignado al usuario (Foreign Key)
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Navegación: Rol del usuario
    /// </summary>
    public virtual Role Role { get; set; } = null!;
}

