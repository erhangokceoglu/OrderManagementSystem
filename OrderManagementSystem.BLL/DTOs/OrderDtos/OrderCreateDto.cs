using OrderManagementSystem.BLL.DTOs.HelperDtos;
using OrderManagementSystem.BLL.DTOs.OrderInfos;
namespace OrderManagementSystem.BLL.DTOs.OrderDtos;

public class OrderCreateDto
{
    public Guid CustomerId { get; set; }
    public List<OrderInfoCreateDto> OrderInfos { get; set; } = new();
}
