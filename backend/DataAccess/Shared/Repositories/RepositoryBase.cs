using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Repositories;

public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(ApplicationDbContext dbContext)
    {
        _dbSet = dbContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public async Task<TEntity> Get(TKey id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            return entity;
        }

        var errorMessage = CreateEntityNotFoundErrorMessage(id);
        throw new EntityNotFoundException(errorMessage);
    }

    public async Task<List<TEntity>> GetMany(IEnumerable<TKey> ids)
    {
        var distinctIds = ids.Distinct().ToList();
        if (distinctIds.Count == 0)
        {
            return [];
        }

        return await _dbSet.Join(distinctIds, e => e.Id, id => id, (e, _) => e).ToListAsync();
    }

    public async Task Delete(TKey id)
    {
        await _dbSet.Where(e => e.Id.Equals(id)).ExecuteDeleteAsync();
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    protected abstract IPointOfSaleErrorMessage CreateEntityNotFoundErrorMessage(TKey id);
}
