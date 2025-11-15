using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.BLL.AbstractServices;
using OrderManagementSystem.BLL.DTOs.OrderDtos;
using OrderManagementSystem.DAL.AbstractRepositories;
using OrderManagementSystem.DAL.Entities;
namespace OrderManagementSystem.BLL.ConcreteServices;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
    {
        var orderRepository = _unitOfWork.GetRepository<Order>();
        var productRepository = _unitOfWork.GetRepository<Product>();
        await using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var order = new Order
            {
                CustomerId = orderCreateDto.CustomerId,
                OrderInfos = new List<OrderInfo>()
            };

            foreach (var orderInfo in orderCreateDto.OrderInfos)
            {
                var product = await productRepository.GetByIdAsync(orderInfo.ProductId);

                if (product == null)
                {
                    throw new Exception($"Ürün bulunamadı: {orderInfo.ProductId}");
                }

                if (product.Stock < orderInfo.Quantity)
                {
                    throw new Exception($"{product.Name} için stok yetersiz.");
                }

                product.Stock -= orderInfo.Quantity;
                productRepository.Update(product);

                order.OrderInfos.Add(new OrderInfo
                {
                    ProductId = product.Id,
                    Quantity = orderInfo.Quantity,
                    UnitPrice = product.Price
                });
            }

            await orderRepository.AddAsync(order);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<OrderDto>(order);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<List<OrderDto>> GetByCustomerIdAsync(Guid customerId)
    {
        var orderRepository = _unitOfWork.GetRepository<Order>();

        await using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var orders = await orderRepository
                .GetAllAsync()
                .Where(x => x.CustomerId == customerId)
                .Include(x => x.OrderInfos)
                .ToListAsync();

            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<OrderDto?> GetByOrderIdAsync(Guid orderId)
    {
        var orderRepository = _unitOfWork.GetRepository<Order>();

        await using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var order = await orderRepository
                .GetAllAsync()
                .Include(o => o.OrderInfos)
                .SingleOrDefaultAsync(o => o.Id == orderId);

            await _unitOfWork.CommitTransactionAsync();

            return _mapper.Map<OrderDto>(order);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> DeleteOrderAsync(Guid orderId)
    {
        var orderRepository = _unitOfWork.GetRepository<Order>();

        await using var transaction = await _unitOfWork.BeginTransactionAsync();

        try
        {
            var order = await orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                return false;
            }

            orderRepository.Remove(order);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();

            return true;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
