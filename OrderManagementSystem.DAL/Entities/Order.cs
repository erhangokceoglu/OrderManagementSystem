using System.ComponentModel.DataAnnotations.Schema;
namespace OrderManagementSystem.DAL.Entities;

public class Order : BaseEntity
{
    [ForeignKey("Customer")]
    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public List<OrderInfo> OrderInfos { get; set; } = new();
}
