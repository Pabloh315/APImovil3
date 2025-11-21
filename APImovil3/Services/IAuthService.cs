using APImovil3.Dto;

namespace APImovil3.Services;

/// <summary>
/// Interfaz para el servicio de Autenticación
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Autentica un usuario y genera un token JWT
    /// </summary>
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
}

