namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record ProductDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal BasePrice { get; init; }
    public required decimal PriceDiscountExcluded { get; init; }
    public required decimal Price { get; init; }
    public required int Stock { get; init; }
    public required List<TaxDTO> Taxes { get; set; }
    public required List<ModifierDTO> Modifiers { get; set; }
    public required List<DiscountDTO> Discounts { get; set; }
    public required int BusinessId { get; init; }
}
