namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateServiceDTO
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required TimeSpan Duration { get; init; }
    public required int BusinessId { get; init; }
}