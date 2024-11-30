namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record ProductDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal PriceWithoutTaxes { get; init; }
    public required decimal PriceWithTaxes { get; init; }
    public required int Stock { get; init; }
    public required List<TaxDTO> Taxes { get; set; }
    public required List<ModifierDTO> Modifiers { get; set; }
}
