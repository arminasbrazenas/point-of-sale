using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Repositories;

public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : notnull
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

    public async Task<List<TEntity>> GetMany(IEnumerable<TKey> ids)
    {
        var distinctIds = ids.Distinct().ToList();
        if (distinctIds.Count == 0)
        {
            return [];
        }
        
        return await _dbSet.Join(distinctIds, e => e.Id, id => id, (e, _) => e).ToListAsync();
    }
}
