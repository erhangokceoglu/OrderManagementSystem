namespace OrderManagementSystem.BLL.DTOs.OrderInfos;

public class OrderInfoCreateDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
