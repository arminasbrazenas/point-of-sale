using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.BusinessManagement.Entities;

public class Business : EntityBase<int>
{
    public required User BusinessOwner {get; set;}
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string TelephoneNumber { get; set; }

    public required string Email { get; set; }
}
