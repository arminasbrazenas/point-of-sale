namespace PointOfSale.DataAccess.Shared.Interfaces;

public interface IRepositoryBase<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : notnull
{
    void Add(TEntity entity);
    Task<List<TEntity>> GetMany(IEnumerable<TKey> ids);
}
