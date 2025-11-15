using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.DAL.AbstractRepositories;
using OrderManagementSystem.DAL.Data;
using OrderManagementSystem.DAL.Entities;
using System.Linq.Expressions;
namespace OrderManagementSystem.DAL.ConcreteRepositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Where(x => x.Id == id && !x.IsDeleted)
            .SingleOrDefaultAsync();
    }

    public IQueryable<TEntity> GetAllAsync()
    {
        return _dbSet.Where(x => !x.IsDeleted);
    }

    public async Task<List<TEntity>> GetPassiveAsync()
    {
        return await _dbSet
            .Where(x => !x.IsActive && !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet
            .Where(x => !x.IsDeleted)
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet
            .Where(x => !x.IsDeleted)
            .SingleOrDefaultAsync(predicate);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(predicate);
    }

    public async Task AddAsync(TEntity entity)
    {
        entity.IsActive = true;
        entity.IsDeleted = false;
        entity.CreatedDate = DateTime.UtcNow;
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.IsActive = true;
            entity.IsDeleted = false;
            entity.CreatedDate = DateTime.UtcNow;
        }
        await _dbSet.AddRangeAsync(entities);
    }

    public void Update(TEntity entity)
    {
        entity.ModifiedDate = DateTime.UtcNow;
        _dbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.IsActive = false;
        entity.DeletedDate = DateTime.UtcNow;
        _dbSet.Update(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.IsDeleted = true;
            entity.IsActive = false;
            entity.DeletedDate = DateTime.UtcNow;
        }
        _dbSet.UpdateRange(entities);
    }
}

