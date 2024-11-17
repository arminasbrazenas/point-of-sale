namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record ProductDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Stock { get; init; }
}