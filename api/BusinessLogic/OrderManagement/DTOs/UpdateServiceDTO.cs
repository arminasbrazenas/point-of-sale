namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public class UpdateServiceDTO
{
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public int? DurationInMinutes { get; init; }
    public List<int>? ProvidedByEmployeesWithId { get; init; }
}
