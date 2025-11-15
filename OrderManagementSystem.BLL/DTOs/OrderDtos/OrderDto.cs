using OrderManagementSystem.BLL.DTOs.HelperDtos;
using OrderManagementSystem.BLL.DTOs.OrderInfoDtos;
namespace OrderManagementSystem.BLL.DTOs.OrderDtos;

public class OrderDto : BaseDto
{
    public Guid CustomerId { get; set; }
    public List<OrderInfoDto> OrderInfos { get; set; } = new();
}
