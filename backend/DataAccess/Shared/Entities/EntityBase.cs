using PointOfSale.DataAccess.Shared.Interfaces;

namespace PointOfSale.DataAccess.Shared.Entities;

public abstract class EntityBase<TKey> : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
}
