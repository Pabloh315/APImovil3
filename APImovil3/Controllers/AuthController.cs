using Microsoft.AspNetCore.Mvc;
using APImovil3.Dto;
using APImovil3.Services;

namespace APImovil3.Controllers;

/// <summary>
/// Controlador para gestionar la autenticación
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Constructor que recibe el servicio de autenticación y el logger
    /// </summary>
    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Autentica un usuario y genera un token JWT
    /// </summary>
    /// <param name="loginDto">Credenciales de login (email y contraseña)</param>
    /// <returns>Token JWT y datos del usuario</returns>
    /// <response code="200">Login exitoso</response>
    /// <response code="400">Credenciales inválidas</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<AuthResponseDto>
                {
                    Success = false,
                    Message = "Datos inválidos"
                });
            }

            var authResult = await _authService.LoginAsync(loginDto);
            if (authResult == null)
            {
                return BadRequest(new ApiResponse<AuthResponseDto>
                {
                    Success = false,
                    Message = "Email o contraseña incorrectos"
                });
            }

            return Ok(new ApiResponse<AuthResponseDto>
            {
                Success = true,
                Message = "Login exitoso",
                Data = authResult
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al realizar login");
            return StatusCode(500, new ApiResponse<AuthResponseDto>
            {
                Success = false,
                Message = "Error interno del servidor"
            });
        }
    }
}

