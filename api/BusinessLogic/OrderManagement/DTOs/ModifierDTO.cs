namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record ModifierDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal PriceTaxExcluded { get; init; }
    public required decimal Price { get; init; }
    public required int Stock { get; init; }
    public required int BusinessId { get; init; }
}
