namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record ServiceDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required TimeSpan Duration { get; init; }
}