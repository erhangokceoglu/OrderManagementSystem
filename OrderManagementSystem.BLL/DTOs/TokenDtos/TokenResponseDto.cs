namespace OrderManagementSystem.BLL.DTOs.TokenDtos;

public class TokenResponseDto
{
    public string? TokenType { get; set; }
    public int ExpiresIn { get; set; }
    public string? AccessToken { get; set; }
}
