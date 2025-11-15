using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderManagementSystem.BLL.AbstractServices;
using OrderManagementSystem.BLL.DTOs.TokenDtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

//Her access token expire süresi dolduğunda worker yeni token almak yerine refresh token ile yenileme yaparım.
//Bu sayede Access Token istek sayın artmıyor API saatte 5 limit koyduğu token isteğini aşmıyorsun.
//Redis ilede refresh tokenlar saklanabilirdi ben uygulamadım.

public class AuthService : IAuthService
{
    private static readonly Dictionary<string, DateTime> _refreshTokens = new();
    private static int _tokenRequestCount = 0;
    private static DateTime _tokenRequestWindowStart = DateTime.UtcNow;
    private readonly string _secretKey;
    private readonly int _accessTokenExpiryMinutes;
    private readonly int _refreshTokenExpiryMinutes;
    private readonly int _tokenLimitPerHour;

    public AuthService(IConfiguration configuration)
    {
        var jwt = configuration.GetSection("JwtSettings");

        _secretKey = jwt["SecretKey"]!;
        _accessTokenExpiryMinutes = int.Parse(jwt["AccessTokenExpiryMinutes"]!);
        _refreshTokenExpiryMinutes = int.Parse(jwt["RefreshTokenExpiryMinutes"]!);
        _tokenLimitPerHour = int.Parse(jwt["TokenRequestLimitPerHour"]!);
    }


    // Yeni access token ve refresh token üretir.
    // İşlem yapılmadan önce rate limit kontrolü yapılır.
    public async Task<TokenResponseDto> GetTokenAsync()
    {
        CheckRateLimit();

        var accessToken = GenerateJwtToken();

        var refreshToken = GenerateRefreshToken();

        _refreshTokens[refreshToken] = DateTime.UtcNow.AddMinutes(_refreshTokenExpiryMinutes);

        return await Task.FromResult(new TokenResponseDto
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            ExpiresIn = _accessTokenExpiryMinutes * 60,
            RefreshToken = refreshToken
        });
    }

    // Refresh token kullanarak access token yeniler.
    // Refresh token geçerli değilse yenileme yapılmaz.
    // Bu method sayesinde access token sık istenmez.
    public async Task<TokenResponseDto> RefreshTokenAsync(string refreshToken)
    {
        _refreshTokens.Remove(refreshToken);

        var newAccessToken = GenerateJwtToken();
        var newRefreshToken = GenerateRefreshToken();

        _refreshTokens[newRefreshToken] = DateTime.UtcNow.AddMinutes(_refreshTokenExpiryMinutes);

        return await Task.FromResult(new TokenResponseDto
        {
            AccessToken = newAccessToken,
            TokenType = "Bearer",
            ExpiresIn = _accessTokenExpiryMinutes * 60,
            RefreshToken = newRefreshToken
        });
    }

    // Saatlik token isteği limitini kontrol eder.
    private void CheckRateLimit()
    {
        var now = DateTime.UtcNow;

        if ((now - _tokenRequestWindowStart).TotalHours >= 1)
        {
            _tokenRequestWindowStart = now;
            _tokenRequestCount = 0;
        }

        if (_tokenRequestCount >= _tokenLimitPerHour)
        {
            throw new Exception($"Saatlik limit aşıldı! Limit: {_tokenLimitPerHour}");
        }

        _tokenRequestCount++;
    }


    // Access token üretir.
    // İçerisinde kullanıcı bilgileri, token bitiş süresi, issuer ve audience bulunur.
    // API tarafından Authorization header'da kullanılır.
    private string GenerateJwtToken()
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
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
            expires: DateTime.UtcNow.AddMinutes(_accessTokenExpiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Güvenli bir refresh token üretir.
    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}

