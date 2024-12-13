namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateTaxDTO
{
    public required string Name { get; init; }
    public required decimal Rate { get; init; }
    public required int BusinessId { get; init; }
}
