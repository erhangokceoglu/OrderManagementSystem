using Microsoft.EntityFrameworkCore.Storage;
using OrderManagementSystem.DAL.AbstractRepositories;
using OrderManagementSystem.DAL.Data;
using OrderManagementSystem.DAL.Entities;
namespace OrderManagementSystem.DAL.ConcreteRepositories;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly AppDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return _repositories[typeof(TEntity)] as IGenericRepository<TEntity>;
        }

        var repository = new GenericRepository<TEntity>(_context);
        _repositories[typeof(TEntity)] = repository;
        return repository;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            await _context.Database.CommitTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_context.Database.CurrentTransaction != null)
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
