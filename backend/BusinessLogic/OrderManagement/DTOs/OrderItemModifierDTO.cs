namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record OrderItemModifierDTO
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
}
