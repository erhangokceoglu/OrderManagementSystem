using OrderManagementSystem.BLL.DTOs.OrderDtos;
namespace OrderManagementSystem.BLL.AbstractServices;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(OrderCreateDto dto);
    Task<List<OrderDto>> GetByCustomerIdAsync(Guid customerId);
    Task<OrderDto?> GetByOrderIdAsync(Guid orderId);
    Task<bool> DeleteOrderAsync(Guid orderId);
}
