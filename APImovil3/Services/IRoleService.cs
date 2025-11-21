using APImovil3.Dto;

namespace APImovil3.Services;

/// <summary>
/// Interfaz para el servicio de Roles
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// Obtiene todos los roles
    /// </summary>
    Task<IEnumerable<RoleResponseDto>> GetAllAsync();

    /// <summary>
    /// Obtiene un rol por su ID
    /// </summary>
    Task<RoleResponseDto?> GetByIdAsync(int id);

    /// <summary>
    /// Crea un nuevo rol
    /// </summary>
    Task<RoleResponseDto> CreateAsync(CreateRoleDto createRoleDto);

    /// <summary>
    /// Actualiza un rol existente
    /// </summary>
    Task<RoleResponseDto?> UpdateAsync(int id, UpdateRoleDto updateRoleDto);

    /// <summary>
    /// Elimina un rol
    /// </summary>
    Task<bool> DeleteAsync(int id);
}

