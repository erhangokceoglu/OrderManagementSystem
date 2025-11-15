using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using OrderManagementSystem.BLL.AbstractServices;
using OrderManagementSystem.BLL.DTOs.TokenDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace OrderManagementSystem.BLL.ConcreteServices;

public class AuthService : IAuthService
{
    private readonly IMemoryCache _cache;
    private const string _cACHEKEY = "api_token";
    private const string _sECRETKEY = "Erhan1234567890123456789012345678";

    public AuthService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<TokenResponseDto> GetTokenAsync()
    {
        if (_cache.TryGetValue(_cACHEKEY, out TokenResponseDto cachedToken))
        {
            return Task.FromResult(cachedToken);
        }

        var tokenExpiryMinutes = 60;
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_sECRETKEY));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "erhan"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "Erhan",
            audience: "ErhanClient",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(tokenExpiryMinutes),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        var tokenResponse = new TokenResponseDto
        {
            AccessToken = tokenString,
            TokenType = "Bearer",
            ExpiresIn = tokenExpiryMinutes * 60
        };

        _cache.Set(_cACHEKEY, tokenResponse, TimeSpan.FromSeconds(tokenResponse.ExpiresIn - 10));

        return Task.FromResult(tokenResponse);
    }
}