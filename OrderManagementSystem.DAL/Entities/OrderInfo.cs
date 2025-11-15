using System.ComponentModel.DataAnnotations.Schema;
namespace OrderManagementSystem.DAL.Entities;

public class OrderInfo : BaseEntity
{
    [ForeignKey("Order")]
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
