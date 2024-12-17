using PointOfSale.Models.ApplicationUserManagement.Entities;
using PointOfSale.Models.BusinessManagement.Entities;
using PointOfSale.Models.OrderManagement.Entities;
using PointOfSale.Models.PaymentManagement.Enums;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.PaymentManagement.Entities;

public abstract class Payment : EntityBase<int>
{
    public required int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public required decimal Amount { get; set; }
    public required PaymentStatus Status { get; set; }
    public required PaymentMethod Method { get; set; }
    public required int BusinessId { get; set; }
    public Business Business { get; set; } = null!;
    public required int EmployeeId { get; set; }
    public ApplicationUser Employee { get; set; } = null!;
}
