namespace PointOfSale.DataAccess.Shared.Interfaces;

public interface IEntity<TKey> : IAuditable
    where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
}
