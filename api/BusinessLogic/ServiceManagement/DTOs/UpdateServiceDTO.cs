namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record UpdateServiceDTO
{
    public required string Name { get; init; }
    public required DateTime AvailableFrom { get; init; }
    public required DateTime AvailableTo { get; init; }
    public required TimeSpan Duration { get; init; }
    public required float Price { get; init; }
}