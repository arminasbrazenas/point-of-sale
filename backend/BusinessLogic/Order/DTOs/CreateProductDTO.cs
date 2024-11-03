namespace PointOfSale.BusinessLogic.Order.DTOs;

public record CreateProductDTO
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Stock { get; init; }
    public required List<int> TaxIds { get; init; }
}
