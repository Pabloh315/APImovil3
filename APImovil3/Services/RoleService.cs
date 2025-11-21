using APImovil3.Dto;
using APImovil3.Models;
using APImovil3.Repositories;

namespace APImovil3.Services;

/// <summary>
/// Implementación del servicio de Roles
/// </summary>
public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    /// <summary>
    /// Constructor que recibe el repositorio de roles
    /// </summary>
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    /// <summary>
    /// Obtiene todos los roles
    /// </summary>
    public async Task<IEnumerable<RoleResponseDto>> GetAllAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Select(r => MapToDto(r));
    }

    /// <summary>
    /// Obtiene un rol por su ID
    /// </summary>
    public async Task<RoleResponseDto?> GetByIdAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        return role == null ? null : MapToDto(role);
    }

    /// <summary>
    /// Crea un nuevo rol
    /// </summary>
    public async Task<RoleResponseDto> CreateAsync(CreateRoleDto createRoleDto)
    {
        // Verificar si ya existe un rol con el mismo nombre
        var existingRole = await _roleRepository.GetByNameAsync(createRoleDto.Name);
        if (existingRole != null)
        {
            throw new InvalidOperationException($"Ya existe un rol con el nombre '{createRoleDto.Name}'");
        }

        var role = new Role
        {
            Name = createRoleDto.Name,
            Description = createRoleDto.Description
        };

        var createdRole = await _roleRepository.CreateAsync(role);
        return MapToDto(createdRole);
    }

    /// <summary>
    /// Actualiza un rol existente
    /// </summary>
    public async Task<RoleResponseDto?> UpdateAsync(int id, UpdateRoleDto updateRoleDto)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
            return null;

        // Verificar si el nuevo nombre ya existe en otro rol
        var existingRole = await _roleRepository.GetByNameAsync(updateRoleDto.Name);
        if (existingRole != null && existingRole.RoleId != id)
        {
            throw new InvalidOperationException($"Ya existe un rol con el nombre '{updateRoleDto.Name}'");
        }

        role.Name = updateRoleDto.Name;
        role.Description = updateRoleDto.Description;

        var updatedRole = await _roleRepository.UpdateAsync(role);
        return MapToDto(updatedRole);
    }

    /// <summary>
    /// Elimina un rol
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        return await _roleRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Mapea un modelo Role a un DTO RoleResponseDto
    /// </summary>
    private static RoleResponseDto MapToDto(Role role)
    {
        return new RoleResponseDto
        {
            RoleId = role.RoleId,
            Name = role.Name,
            Description = role.Description
        };
    }
}

