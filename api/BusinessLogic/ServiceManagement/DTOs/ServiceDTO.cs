namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record ServiceDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required DateTime AvailableFrom { get; init; }
    public required DateTime AvailableTo { get; init; }
    public required TimeSpan Duration { get; init; }
    public required decimal Price { get; init; }
}