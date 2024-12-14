namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record UpdateServiceDTO
{
    public string? Name { get; init; }
    public DateTime? AvailableFrom { get; init; }
    public DateTime? AvailableTo { get; init; }
    public TimeSpan? Duration { get; init; }
    public decimal? Price { get; init; }
}