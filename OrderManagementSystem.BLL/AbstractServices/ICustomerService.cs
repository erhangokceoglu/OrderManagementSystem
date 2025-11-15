using OrderManagementSystem.BLL.DTOs.CustomerDtos;
namespace OrderManagementSystem.BLL.AbstractServices;

public interface ICustomerService
{
    Task<List<CustomerDto>> GetAllCustomersAsync();
}
