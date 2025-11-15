namespace OrderManagementSystem.DAL.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public List<OrderInfo> OrderInfos { get; set; } = new();
}
