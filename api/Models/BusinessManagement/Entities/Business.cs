using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.BusinessManagement.Entities;

public class Business : EntityBase<int>
{
    public ApplicationUser BusinessOwner { get; set; } = null!;
    public required int BusinessOwnerId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string TelephoneNumber { get; set; }
    public required string Email { get; set; }
    public ICollection<ApplicationUser> Employees { get; set; } = new List<ApplicationUser>();
}
