using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.BLL.AbstractServices;
using OrderManagementSystem.BLL.DTOs.CustomerDtos;
using OrderManagementSystem.DAL.AbstractRepositories;
using OrderManagementSystem.DAL.Entities;
namespace OrderManagementSystem.BLL.ConcreteServices;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CustomerDto>> GetAllAsync()
    {
        try
        {
            var customerRepository = _unitOfWork.GetRepository<Customer>();
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            var customers = await customerRepository.GetAllAsync().ToListAsync();
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<List<CustomerDto>>(customers);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
