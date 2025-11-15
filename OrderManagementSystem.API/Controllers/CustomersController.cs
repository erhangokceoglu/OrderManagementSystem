using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.Models;
using OrderManagementSystem.BLL.AbstractServices;
using OrderManagementSystem.BLL.DTOs.CustomerDtos;
namespace OrderManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var customers = await _customerService.GetAllAsync();

            if (customers == null || customers.Count == 0)
            {
                return NotFound(ApiResponse<string>.NotFound("Müşteriler bulunamadı."));
            }

            return Ok(ApiResponse<List<CustomerDto>>.Ok(customers, "Müşteriler getirildi."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.InternalError("Müşteriler gelmedi." + ex.Message));
        }
    }
}
