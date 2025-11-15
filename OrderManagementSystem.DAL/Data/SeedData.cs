using OrderManagementSystem.DAL.Entities;
namespace OrderManagementSystem.DAL.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        // Ürünler
        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product { Id = Guid.NewGuid(), Name = "Ürün 1", Price = 1200, Stock = 10, IsActive = true, IsDeleted = false, CreatedById = Guid.Empty },
                new Product { Id = Guid.NewGuid(), Name = "Ürün 2", Price = 50, Stock = 50, IsActive = true, IsDeleted = false, CreatedById = Guid.Empty },
                new Product { Id = Guid.NewGuid(), Name = "Ürün 3", Price = 500, Stock = 20, IsActive = true, IsDeleted = false , CreatedById = Guid.Empty }
            );
        }

        // Müşteriler
        if (!context.Customers.Any())
        {
            context.Customers.AddRange(
                new Customer { Id = Guid.NewGuid(), FirstName = "Erhan", LastName = "Gökçeoğlu", Email = "erhan@example.com", IsActive = true, IsDeleted = false, CreatedById = Guid.Empty }
            );
        }

        context.SaveChanges();
    }
}
