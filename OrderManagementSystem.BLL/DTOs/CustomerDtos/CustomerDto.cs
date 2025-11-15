using OrderManagementSystem.BLL.DTOs.HelperDtos;
namespace OrderManagementSystem.BLL.DTOs.CustomerDtos;

public class CustomerDto : BaseDto
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
}