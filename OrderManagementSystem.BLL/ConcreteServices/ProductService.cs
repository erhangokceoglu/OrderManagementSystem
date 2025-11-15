using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.BLL.AbstractServices;
using OrderManagementSystem.BLL.DTOs.ProductDtos;
using OrderManagementSystem.DAL.AbstractRepositories;
using OrderManagementSystem.DAL.Entities;
namespace OrderManagementSystem.BLL.ConcreteServices;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> GetAllProductsAsync()
    {
        try
        {
            var productRepository = _unitOfWork.GetRepository<Product>();
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            var products = await productRepository.GetAllAsync().ToListAsync();
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
