using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.Models;
using OrderManagementSystem.BLL.AbstractServices;
namespace OrderManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthsController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("GetToken")]
    public async Task<IActionResult> GetToken()
    {
        try
        {
            var token = await _authService.GetTokenAsync();
            return Ok(token);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.InternalError("Token gelmedi." + ex.Message));
        }
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        try
        {
            var token = await _authService.RefreshTokenAsync(refreshToken);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.InternalError("RefreshToken gelmedi." + ex.Message));
        }
    }
}