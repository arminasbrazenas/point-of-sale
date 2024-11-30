namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateOrUpdateOrderItemDTO
{
    public required int ProductId { get; init; }
    public required List<int> ModifierIds { get; init; }
    public required int Quantity { get; init; }
}
