using APImovil3.Dto;
using APImovil3.Models;
using APImovil3.Repositories;
using BCrypt.Net;

namespace APImovil3.Services;

/// <summary>
/// Implementación del servicio de Usuarios
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    /// <summary>
    /// Constructor que recibe los repositorios necesarios
    /// </summary>
    public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    /// <summary>
    /// Obtiene todos los usuarios con su rol
    /// </summary>
    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => MapToDto(u));
    }

    /// <summary>
    /// Obtiene un usuario por su ID con su rol
    /// </summary>
    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : MapToDto(user);
    }

    /// <summary>
    /// Obtiene usuarios filtrados por rol
    /// </summary>
    public async Task<IEnumerable<UserResponseDto>> GetByRoleIdAsync(int roleId)
    {
        var users = await _userRepository.GetByRoleIdAsync(roleId);
        return users.Select(u => MapToDto(u));
    }

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    public async Task<UserResponseDto> CreateAsync(CreateUserDto createUserDto)
    {
        // Validar que el email sea único
        if (await _userRepository.EmailExistsAsync(createUserDto.Email))
        {
            throw new InvalidOperationException($"Ya existe un usuario con el email '{createUserDto.Email}'");
        }

        // Validar que el rol exista
        var role = await _roleRepository.GetByIdAsync(createUserDto.RoleId);
        if (role == null)
        {
            throw new InvalidOperationException($"No existe un rol con el ID {createUserDto.RoleId}");
        }

        // Hashear la contraseña con BCrypt
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

        var user = new User
        {
            FullName = createUserDto.FullName,
            Email = createUserDto.Email,
            PasswordHash = passwordHash,
            RoleId = createUserDto.RoleId
        };

        var createdUser = await _userRepository.CreateAsync(user);
        // Recargar el usuario con su rol
        var userWithRole = await _userRepository.GetByIdAsync(createdUser.UserId);
        return MapToDto(userWithRole!);
    }

    /// <summary>
    /// Actualiza un usuario existente
    /// </summary>
    public async Task<UserResponseDto?> UpdateAsync(int id, UpdateUserDto updateUserDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return null;

        // Validar que el email sea único (si cambió)
        if (user.Email != updateUserDto.Email)
        {
            if (await _userRepository.EmailExistsAsync(updateUserDto.Email))
            {
                throw new InvalidOperationException($"Ya existe un usuario con el email '{updateUserDto.Email}'");
            }
        }

        // Validar que el rol exista
        var role = await _roleRepository.GetByIdAsync(updateUserDto.RoleId);
        if (role == null)
        {
            throw new InvalidOperationException($"No existe un rol con el ID {updateUserDto.RoleId}");
        }

        user.FullName = updateUserDto.FullName;
        user.Email = updateUserDto.Email;
        user.RoleId = updateUserDto.RoleId;

        // Actualizar contraseña solo si se proporciona
        if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
        }

        var updatedUser = await _userRepository.UpdateAsync(user);
        // Recargar el usuario con su rol
        var userWithRole = await _userRepository.GetByIdAsync(updatedUser.UserId);
        return MapToDto(userWithRole!);
    }

    /// <summary>
    /// Elimina un usuario
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Mapea un modelo User a un DTO UserResponseDto
    /// </summary>
    private static UserResponseDto MapToDto(User user)
    {
        return new UserResponseDto
        {
            UserId = user.UserId,
            FullName = user.FullName,
            Email = user.Email,
            RoleId = user.RoleId,
            Role = user.Role != null ? new RoleResponseDto
            {
                RoleId = user.Role.RoleId,
                Name = user.Role.Name,
                Description = user.Role.Description
            } : null
        };
    }
}

