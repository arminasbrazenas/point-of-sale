namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record OrderItemDTO
{
    public required int Id { get; init; }
    public required int? ProductId { get; init; }
    public required string Name { get; init; }
    public required int Quantity { get; init; }
    public required decimal UnitPrice { get; init; }
    public required decimal DiscountsTotal { get; init; }
    public required decimal TaxTotal { get; init; }
    public required decimal TotalPrice { get; init; }
    public required List<OrderItemModifierDTO> Modifiers { get; init; }
    public required List<OrderDiscountDTO> Discounts { get; init; }
    public required List<OrderItemTaxDTO> Taxes { get; init; }
}
