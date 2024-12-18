namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record OrderItemTaxDTO
{
    public required string Name { get; init; }
    public required decimal RatePercentage { get; init; }
    public required decimal AppliedAmount { get; init; }
}
