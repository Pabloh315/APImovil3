using System.ComponentModel.DataAnnotations;

namespace APImovil3.Dto;

/// <summary>
/// DTO para crear un nuevo usuario
/// </summary>
public class CreateUserDto
{
    /// <summary>
    /// Nombre completo del usuario
    /// </summary>
    [Required(ErrorMessage = "El nombre completo es requerido")]
    [StringLength(150, ErrorMessage = "El nombre completo no puede exceder 150 caracteres")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Email del usuario (debe ser único)
    /// </summary>
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    [StringLength(150, ErrorMessage = "El email no puede exceder 150 caracteres")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña del usuario (se hasheará antes de guardar)
    /// </summary>
    [Required(ErrorMessage = "La contraseña es requerida")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Identificador del rol a asignar
    /// </summary>
    [Required(ErrorMessage = "El ID del rol es requerido")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del rol debe ser mayor a 0")]
    public int RoleId { get; set; }
}

/// <summary>
/// DTO para actualizar un usuario existente
/// </summary>
public class UpdateUserDto
{
    /// <summary>
    /// Nombre completo del usuario
    /// </summary>
    [Required(ErrorMessage = "El nombre completo es requerido")]
    [StringLength(150, ErrorMessage = "El nombre completo no puede exceder 150 caracteres")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Email del usuario (debe ser único)
    /// </summary>
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    [StringLength(150, ErrorMessage = "El email no puede exceder 150 caracteres")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña del usuario (opcional, solo se actualiza si se proporciona)
    /// </summary>
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    public string? Password { get; set; }

    /// <summary>
    /// Identificador del rol a asignar
    /// </summary>
    [Required(ErrorMessage = "El ID del rol es requerido")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del rol debe ser mayor a 0")]
    public int RoleId { get; set; }
}

/// <summary>
/// DTO para respuesta de usuario (sin información sensible)
/// </summary>
public class UserResponseDto
{
    /// <summary>
    /// Identificador único del usuario
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Nombre completo del usuario
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Email del usuario
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Identificador del rol
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Información del rol del usuario
    /// </summary>
    public RoleResponseDto? Role { get; set; }
}

/// <summary>
/// DTO para login
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Email del usuario
    /// </summary>
    [Required(ErrorMessage = "El email es requerido")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña del usuario
    /// </summary>
    [Required(ErrorMessage = "La contraseña es requerida")]
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// DTO para respuesta de autenticación
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// Token JWT generado
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Información del usuario autenticado
    /// </summary>
    public UserResponseDto User { get; set; } = null!;
}

/// <summary>
/// DTO para respuesta genérica de la API
/// </summary>
public class ApiResponse<T>
{
    /// <summary>
    /// Indica si la operación fue exitosa
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensaje de respuesta
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Datos de la respuesta
    /// </summary>
    public T? Data { get; set; }
}

