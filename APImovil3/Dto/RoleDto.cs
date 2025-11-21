using System.ComponentModel.DataAnnotations;

namespace APImovil3.Dto;

/// <summary>
/// DTO para crear un nuevo rol
/// </summary>
public class CreateRoleDto
{
    /// <summary>
    /// Nombre del rol
    /// </summary>
    [Required(ErrorMessage = "El nombre del rol es requerido")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del rol
    /// </summary>
    [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
    public string? Description { get; set; }
}

/// <summary>
/// DTO para actualizar un rol existente
/// </summary>
public class UpdateRoleDto
{
    /// <summary>
    /// Nombre del rol
    /// </summary>
    [Required(ErrorMessage = "El nombre del rol es requerido")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del rol
    /// </summary>
    [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
    public string? Description { get; set; }
}

/// <summary>
/// DTO para respuesta de rol
/// </summary>
public class RoleResponseDto
{
    /// <summary>
    /// Identificador único del rol
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Nombre del rol
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del rol
    /// </summary>
    public string? Description { get; set; }
}

