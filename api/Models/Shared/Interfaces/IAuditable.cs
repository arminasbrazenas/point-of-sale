using PointOfSale.Models.ApplicationUserManagement.Entities;

namespace PointOfSale.Models.Shared.Interfaces;

public interface IAuditable
{
    public DateTimeOffset CreatedAt { get; set; }
    public ApplicationUser CreatedBy { get; set; }
    public int? CreatedById { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public ApplicationUser ModifiedBy { get; set; }
    public int? ModifiedById { get; set; }
}
