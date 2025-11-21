using Microsoft.AspNetCore.Mvc;
using APImovil3.Dto;
using APImovil3.Services;

namespace APImovil3.Controllers;

/// <summary>
/// Controlador para gestionar Usuarios
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UsersController> _logger;

    /// <summary>
    /// Constructor que recibe el servicio de usuarios y el logger
    /// </summary>
    public UsersController(IUserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene todos los usuarios con su rol
    /// </summary>
    /// <returns>Lista de usuarios</returns>
    /// <response code="200">Lista de usuarios obtenida exitosamente</response>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserResponseDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserResponseDto>>>> GetAll()
    {
        try
        {
            var users = await _userService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<UserResponseDto>>
            {
                Success = true,
                Message = "Usuarios obtenidos exitosamente",
                Data = users
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuarios");
            return StatusCode(500, new ApiResponse<IEnumerable<UserResponseDto>>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Obtiene un usuario por su ID con su rol
    /// </summary>
    /// <param name="id">ID del usuario</param>
    /// <returns>Usuario encontrado</returns>
    /// <response code="200">Usuario obtenido exitosamente</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> GetById(int id)
    {
        try
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Message = $"Usuario con ID {id} no encontrado"
                });
            }

            return Ok(new ApiResponse<UserResponseDto>
            {
                Success = true,
                Message = "Usuario obtenido exitosamente",
                Data = user
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuario con ID {UserId}", id);
            return StatusCode(500, new ApiResponse<UserResponseDto>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Obtiene usuarios filtrados por rol
    /// </summary>
    /// <param name="roleId">ID del rol para filtrar</param>
    /// <returns>Lista de usuarios del rol especificado</returns>
    /// <response code="200">Usuarios obtenidos exitosamente</response>
    [HttpGet("by-role/{roleId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserResponseDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserResponseDto>>>> GetByRoleId(int roleId)
    {
        try
        {
            var users = await _userService.GetByRoleIdAsync(roleId);
            return Ok(new ApiResponse<IEnumerable<UserResponseDto>>
            {
                Success = true,
                Message = $"Usuarios del rol {roleId} obtenidos exitosamente",
                Data = users
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuarios por rol {RoleId}", roleId);
            return StatusCode(500, new ApiResponse<IEnumerable<UserResponseDto>>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    /// <param name="createUserDto">Datos del usuario a crear</param>
    /// <returns>Usuario creado</returns>
    /// <response code="200">Usuario creado exitosamente</response>
    /// <response code="400">Datos inválidos, email duplicado o rol no existe</response>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> Create([FromBody] CreateUserDto createUserDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Message = "Datos inválidos"
                });
            }

            var user = await _userService.CreateAsync(createUserDto);
            return Ok(new ApiResponse<UserResponseDto>
            {
                Success = true,
                Message = "Usuario creado exitosamente",
                Data = user
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<UserResponseDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear usuario");
            return StatusCode(500, new ApiResponse<UserResponseDto>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Actualiza un usuario existente
    /// </summary>
    /// <param name="id">ID del usuario a actualizar</param>
    /// <param name="updateUserDto">Datos actualizados del usuario</param>
    /// <returns>Usuario actualizado</returns>
    /// <response code="200">Usuario actualizado exitosamente</response>
    /// <response code="400">Datos inválidos, email duplicado o rol no existe</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<UserResponseDto>>> Update(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Message = "Datos inválidos"
                });
            }

            var user = await _userService.UpdateAsync(id, updateUserDto);
            if (user == null)
            {
                return NotFound(new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Message = $"Usuario con ID {id} no encontrado"
                });
            }

            return Ok(new ApiResponse<UserResponseDto>
            {
                Success = true,
                Message = "Usuario actualizado exitosamente",
                Data = user
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<UserResponseDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar usuario con ID {UserId}", id);
            return StatusCode(500, new ApiResponse<UserResponseDto>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }

    /// <summary>
    /// Elimina un usuario
    /// </summary>
    /// <param name="id">ID del usuario a eliminar</param>
    /// <returns>Resultado de la eliminación</returns>
    /// <response code="200">Usuario eliminado exitosamente</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        try
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = $"Usuario con ID {id} no encontrado"
                });
            }

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Usuario eliminado exitosamente"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar usuario con ID {UserId}", id);
            return StatusCode(500, new ApiResponse<object>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }
}

