namespace OrderManagementSystem.BLL.DTOs.TokenDtos;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = null!;
    public string TokenType { get; set; } = null!;
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = null!;
}
