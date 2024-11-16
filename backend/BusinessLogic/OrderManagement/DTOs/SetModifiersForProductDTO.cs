namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record SetModifiersForProductDTO
{
    public required List<int> ModifierIds { get; init; }
}
