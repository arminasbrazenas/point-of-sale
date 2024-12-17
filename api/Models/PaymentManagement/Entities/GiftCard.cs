using PointOfSale.Models.BusinessManagement.Entities;
using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.PaymentManagement.Entities;

public class GiftCard : EntityBase<int>
{
    public required string Code { get; set; }
    public required decimal Amount { get; set; }
    public required DateTimeOffset ExpiresAt { get; set; }
    public required DateTimeOffset? UsedAt { get; set; }
    public required int BusinessId { get; set; }
    public Business Business { get; set; } = null!;
}
