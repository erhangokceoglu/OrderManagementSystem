using OrderManagementSystem.BLL.DTOs.TokenDtos;
namespace OrderManagementSystem.BLL.AbstractServices;

public interface IAuthService
{
    Task<TokenResponseDto> GetTokenAsync();
    Task<TokenResponseDto> RefreshTokenAsync(string refreshToken);
}
