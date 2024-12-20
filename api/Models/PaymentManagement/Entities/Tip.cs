using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.PaymentManagement.Entities;

public class Tip : EntityBase<int>
{
    public required int OrderId { get; init; }
    public Order Order { get; init; } = null!;
    public required decimal Amount { get; init; }
    public required int EmployeeId { get; set; }
    public ApplicationUser Employee { get; set; } = null!;
}
