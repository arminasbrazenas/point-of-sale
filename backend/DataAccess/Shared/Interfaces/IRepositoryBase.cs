namespace PointOfSale.DataAccess.Shared.Interfaces;

public interface IRepositoryBase<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    void Add(TEntity entity);
    Task<TEntity> Get(TKey id);
    Task<List<TEntity>> GetMany(IEnumerable<TKey> ids);
    Task Delete(TKey id);
    void Update(TEntity entity);
}
