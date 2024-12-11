using PointOfSale.Models.Shared.Entities;

namespace PointOfSale.Models.ApplicationUserManagement.Entities;

public class RefreshToken : EntityBase<int>
{
    public required string RefreshTokenHash { get; set; }
    public required int ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public required DateTime ExpiryDate { get; set; }
    public required bool IsRevoked { get; set; }
}
