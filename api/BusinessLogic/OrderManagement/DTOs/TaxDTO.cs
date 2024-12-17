namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record TaxDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Rate { get; init; }
    public required int BusinessId {get; init;}
}
