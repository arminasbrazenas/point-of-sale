namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public class UpdateServiceDTO
{
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public TimeSpan? Duration { get; init; }
}