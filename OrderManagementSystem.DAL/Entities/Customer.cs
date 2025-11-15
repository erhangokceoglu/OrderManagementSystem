namespace OrderManagementSystem.DAL.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public List<Order>? Orders { get; set; } = new();
}
