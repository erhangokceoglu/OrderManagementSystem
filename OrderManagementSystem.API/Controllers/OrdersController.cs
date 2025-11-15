using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.Models;
using OrderManagementSystem.BLL.AbstractServices;
using OrderManagementSystem.BLL.DTOs.OrderDtos;
namespace OrderManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<string>.BadRequest("Geçersiz istek."));

        try
        {
            var result = await _orderService.CreateAsync(dto);

            if (!string.IsNullOrEmpty(result.Error))
            {
                return BadRequest(ApiResponse<string>.BadRequest(result.Error));
            }

            return Ok(ApiResponse<OrderDto>.Ok(result.OrderDto, "Sipariş başarıyla oluşturuldu."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.InternalError("Beklenmeyen hata: " + ex.Message));
        }
    }

    [HttpGet("GetByCustomerId/{customerId}")]
    public async Task<IActionResult> GetByCustomerId(Guid customerId)
    {
        try
        {
            var orders = await _orderService.GetByCustomerIdAsync(customerId);

            if (orders == null || orders.Count == 0)
            {
                return NotFound(ApiResponse<string>.NotFound("Bu müşteriye ait sipariş bulunamadı."));
            }

            return Ok(ApiResponse<List<OrderDto>>.Ok(orders, "Müşteriye ait siparişler getirildi."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.InternalError("Müşteriye ait siparişler getirilemedi." + ex.Message));
        }
    }

    [HttpGet("GetByOrderId/{orderId}")]
    public async Task<IActionResult> GetByOrderId(Guid orderId)
    {
        try
        {
            var order = await _orderService.GetByOrderIdAsync(orderId);

            if (order == null)
            {
                return NotFound(ApiResponse<string>.NotFound("Sipariş bulunamadı."));
            }

            return Ok(ApiResponse<OrderDto>.Ok(order, "Sipariş bulundu."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.InternalError("Sipariş getirilemedi." + ex.Message));
        }
    }

    [HttpDelete("Delete/{orderId}")]
    public async Task<IActionResult> Delete(Guid orderId)
    {
        try
        {
            var deletedOrder = await _orderService.DeleteAsync(orderId);

            if (!deletedOrder)
            {
                return NotFound(ApiResponse<string>.NotFound("Silinecek sipariş bulunamadı."));
            }

            return Ok(ApiResponse<string>.Ok(null, "Sipariş başarıyla silindi."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<string>.InternalError("Sipariş silinemedi." + ex.Message));
        }
    }
}

