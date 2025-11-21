using Microsoft.AspNetCore.Mvc;
using APImovil3.Dto;
using APImovil3.Services;

namespace APImovil3.Controllers;

/// <summary>
/// Controlador para gestionar Roles
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly ILogger<RolesController> _logger;

    /// <summary>
    /// Constructor que recibe el servicio de roles y el logger
    /// </summary>
    public RolesController(IRoleService roleService, ILogger<RolesController> logger)
    {
        _roleService = roleService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los roles
    /// </summary>
    /// <returns>Lista de roles</returns>
    /// <response code="200">Lista de roles obtenida exitosamente</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoleResponseDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<RoleResponseDto>>>> GetAll()
    {
        try
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<RoleResponseDto>>
            {
                Success = true,
                Message = "Roles obtenidos exitosamente",
                Data = roles
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener roles");
            return StatusCode(500, new ApiResponse<IEnumerable<RoleResponseDto>>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Obtiene un rol por su ID
    /// </summary>
    /// <param name="id">ID del rol</param>
    /// <returns>Rol encontrado</returns>
    /// <response code="200">Rol obtenido exitosamente</response>
    /// <response code="404">Rol no encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<RoleResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<RoleResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<RoleResponseDto>>> GetById(int id)
    {
        try
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound(new ApiResponse<RoleResponseDto>
                {
                    Success = false,
                    Message = $"Rol con ID {id} no encontrado"
                });
            }

            return Ok(new ApiResponse<RoleResponseDto>
            {
                Success = true,
                Message = "Rol obtenido exitosamente",
                Data = role
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener rol con ID {RoleId}", id);
            return StatusCode(500, new ApiResponse<RoleResponseDto>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Crea un nuevo rol
    /// </summary>
    /// <param name="createRoleDto">Datos del rol a crear</param>
    /// <returns>Rol creado</returns>
    /// <response code="200">Rol creado exitosamente</response>
    /// <response code="400">Datos inválidos o rol duplicado</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<RoleResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<RoleResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<RoleResponseDto>>> Create([FromBody] CreateRoleDto createRoleDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<RoleResponseDto>
                {
                    Success = false,
                    Message = "Datos inválidos",
                    Data = null
                });
            }

            var role = await _roleService.CreateAsync(createRoleDto);
            return Ok(new ApiResponse<RoleResponseDto>
            {
                Success = true,
                Message = "Rol creado exitosamente",
                Data = role
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<RoleResponseDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear rol");
            return StatusCode(500, new ApiResponse<RoleResponseDto>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Actualiza un rol existente
    /// </summary>
    /// <param name="id">ID del rol a actualizar</param>
    /// <param name="updateRoleDto">Datos actualizados del rol</param>
    /// <returns>Rol actualizado</returns>
    /// <response code="200">Rol actualizado exitosamente</response>
    /// <response code="400">Datos inválidos o rol duplicado</response>
    /// <response code="404">Rol no encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<RoleResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<RoleResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<RoleResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<RoleResponseDto>>> Update(int id, [FromBody] UpdateRoleDto updateRoleDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<RoleResponseDto>
                {
                    Success = false,
                    Message = "Datos inválidos"
                });
            }

            var role = await _roleService.UpdateAsync(id, updateRoleDto);
            if (role == null)
            {
                return NotFound(new ApiResponse<RoleResponseDto>
                {
                    Success = false,
                    Message = $"Rol con ID {id} no encontrado"
                });
            }

            return Ok(new ApiResponse<RoleResponseDto>
            {
                Success = true,
                Message = "Rol actualizado exitosamente",
                Data = role
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<RoleResponseDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar rol con ID {RoleId}", id);
            return StatusCode(500, new ApiResponse<RoleResponseDto>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Elimina un rol
    /// </summary>
    /// <param name="id">ID del rol a eliminar</param>
    /// <returns>Resultado de la eliminación</returns>
    /// <response code="200">Rol eliminado exitosamente</response>
    /// <response code="404">Rol no encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        try
        {
            var deleted = await _roleService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Rol con ID {id} no encontrado"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Rol eliminado exitosamente"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar rol con ID {RoleId}", id);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }
}

