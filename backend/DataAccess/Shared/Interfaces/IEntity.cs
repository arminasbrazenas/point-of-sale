namespace PointOfSale.DataAccess.Shared.Interfaces;

public interface IEntity<TKey> : IAuditable
    where TKey : notnull
{
    TKey Id { get; set; }
}
