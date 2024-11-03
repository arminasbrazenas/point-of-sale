namespace PointOfSale.DataAccess.Shared.Interfaces;

public interface IRepositoryBase<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : notnull
{
    void Add(TEntity entity);
}
