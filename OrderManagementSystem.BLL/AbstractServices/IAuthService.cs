using OrderManagementSystem.BLL.DTOs.TokenDtos;
namespace OrderManagementSystem.BLL.AbstractServices;

public interface IAuthService
{
    Task<TokenResponseDto> GetTokenAsync();
}
