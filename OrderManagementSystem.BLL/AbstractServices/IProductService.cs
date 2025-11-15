using OrderManagementSystem.BLL.DTOs.ProductDtos;
namespace OrderManagementSystem.BLL.AbstractServices;

public interface IProductService
{
    Task<List<ProductDto>> GetAllProductsAsync();
}
