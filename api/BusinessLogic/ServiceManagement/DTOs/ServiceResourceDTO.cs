namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record ServiceResourceDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}