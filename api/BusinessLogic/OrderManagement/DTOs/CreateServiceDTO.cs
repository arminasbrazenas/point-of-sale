namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateServiceDTO
{
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public required int DurationInMinutes { get; init; }
    public required int BusinessId { get; init; }
    public required List<int> ProvidedByEmployeesWithId { get; init; }
}
