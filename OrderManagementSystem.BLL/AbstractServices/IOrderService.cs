using OrderManagementSystem.BLL.DTOs.OrderDtos;
namespace OrderManagementSystem.BLL.AbstractServices;

public interface IOrderService
{
    Task<OrderResponseDto> CreateAsync(OrderCreateDto orderCreateDto);
    Task<List<OrderDto>> GetByCustomerIdAsync(Guid customerId);
    Task<OrderDto?> GetByOrderIdAsync(Guid orderId);
    Task<bool> DeleteAsync(Guid orderId);
}
