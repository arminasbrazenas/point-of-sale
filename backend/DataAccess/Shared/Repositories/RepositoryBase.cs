using Microsoft.EntityFrameworkCore;
using PointOfSale.DataAccess.Shared.DTOs;
using PointOfSale.DataAccess.Shared.Exceptions;
using PointOfSale.DataAccess.Shared.Interfaces;
using PointOfSale.Models.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Repositories;

public abstract class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly DbSet<TEntity> DbSet;

    protected RepositoryBase(ApplicationDbContext dbContext)
    {
        DbSet = dbContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public async Task<TEntity> Get(TKey id)
    {
        var entity = await DbSet.FindAsync(id);
        if (entity is not null)
        {
            return entity;
        }

        var errorMessage = GetEntityNotFoundErrorMessage(id);
        throw new EntityNotFoundException(errorMessage);
    }

    public async Task<List<TEntity>> GetMany(IEnumerable<TKey> ids)
    {
        var distinctIds = ids.Distinct().ToList();
        if (distinctIds.Count == 0)
        {
            return [];
        }

        return await DbSet.Join(distinctIds, e => e.Id, id => id, (e, _) => e).ToListAsync();
    }

    public async Task Delete(TKey id)
    {
        await DbSet.Where(e => e.Id.Equals(id)).ExecuteDeleteAsync();
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    protected async Task<List<TEntity>> GetPaginated(IQueryable<TEntity> query, PaginationFilter paginationFilter)
    {
        query = query
            .Skip((paginationFilter.Page - 1) * paginationFilter.ItemsPerPage)
            .Take(paginationFilter.ItemsPerPage)
            .AsNoTracking();

        return await query.ToListAsync();
    }

    protected abstract IPointOfSaleErrorMessage GetEntityNotFoundErrorMessage(TKey id);
}
