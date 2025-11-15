using Microsoft.AspNetCore.Mvc;
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
        var token = await _authService.GetTokenAsync();
        return Ok(token);
    }
}