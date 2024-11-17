namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record ModifierDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int Amount { get; init; }
}