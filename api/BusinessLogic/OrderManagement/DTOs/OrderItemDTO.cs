namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record OrderItemDTO
{
    public required int Id { get; init; }
    public required int? ProductId { get; set; }
    public required string Name { get; init; }
    public required int Quantity { get; init; }
    public required decimal BaseUnitPrice { get; set; }
    public required decimal TotalPrice { get; init; }
    public required List<OrderItemModifierDTO> Modifiers { get; init; }
}
