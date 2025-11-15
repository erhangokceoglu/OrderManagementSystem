using Microsoft.EntityFrameworkCore.Storage;
using OrderManagementSystem.DAL.Entities;
namespace OrderManagementSystem.DAL.AbstractRepositories;

public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    Task<int> CompleteAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
