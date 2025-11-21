using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using APImovil3.Dto;
using APImovil3.Repositories;
using BCrypt.Net;

namespace APImovil3.Services;

/// <summary>
/// Implementación del servicio de Autenticación
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Constructor que recibe el repositorio de usuarios y la configuración
    /// </summary>
    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    /// <summary>
    /// Autentica un usuario y genera un token JWT
    /// </summary>
    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        // Buscar usuario por email
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user == null)
            return null;

        // Verificar contraseña con BCrypt
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            return null;

        // Generar token JWT
        var token = GenerateJwtToken(user);

        // Mapear usuario a DTO
        var userDto = new UserResponseDto
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

        return new AuthResponseDto
        {
            Token = token,
            User = userDto
        };
    }

    /// <summary>
    /// Genera un token JWT para el usuario
    /// </summary>
    private string GenerateJwtToken(Models.User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey no configurada");
        var issuer = jwtSettings["Issuer"] ?? "APImovil3";
        var audience = jwtSettings["Audience"] ?? "APImovil3";
        var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"] ?? "60");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role?.Name ?? "User")
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

