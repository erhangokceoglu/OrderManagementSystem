using OrderManagementSystem.BLL.DTOs.HelperDtos;
namespace OrderManagementSystem.BLL.DTOs.ProductDtos;

public class ProductDto : BaseDto
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
