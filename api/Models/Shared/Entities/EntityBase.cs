using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.Shared.Interfaces;

namespace PointOfSale.Models.Shared.Entities;

public abstract class EntityBase<TKey> : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public ApplicationUser CreatedBy { get; set; } = null!;
    public int? CreatedById { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public ApplicationUser ModifiedBy { get; set; } = null!;
    public int? ModifiedById { get; set; }
}
