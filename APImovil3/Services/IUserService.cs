using APImovil3.Dto;

namespace APImovil3.Services;

/// <summary>
/// Interfaz para el servicio de Usuarios
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Obtiene todos los usuarios con su rol
    /// </summary>
    Task<IEnumerable<UserResponseDto>> GetAllAsync();

    /// <summary>
    /// Obtiene un usuario por su ID con su rol
    /// </summary>
    Task<UserResponseDto?> GetByIdAsync(int id);

    /// <summary>
    /// Obtiene usuarios filtrados por rol
    /// </summary>
    Task<IEnumerable<UserResponseDto>> GetByRoleIdAsync(int roleId);

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    Task<UserResponseDto> CreateAsync(CreateUserDto createUserDto);

    /// <summary>
    /// Actualiza un usuario existente
    /// </summary>
    Task<UserResponseDto?> UpdateAsync(int id, UpdateUserDto updateUserDto);

    /// <summary>
    /// Elimina un usuario
    /// </summary>
    Task<bool> DeleteAsync(int id);
}

