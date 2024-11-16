namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateModifierDTO
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Amount { get; init; }
}
