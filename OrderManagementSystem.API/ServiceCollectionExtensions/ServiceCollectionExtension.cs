using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrderManagementSystem.BLL.AbstractServices;
using OrderManagementSystem.BLL.ConcreteServices;
using OrderManagementSystem.DAL.AbstractRepositories;
using OrderManagementSystem.DAL.ConcreteRepositories;
using OrderManagementSystem.DAL.Data;
namespace OrderManagementSystem.API.ServiceCollectionExtensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("OMSDb").ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
        return services;
    }

    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
