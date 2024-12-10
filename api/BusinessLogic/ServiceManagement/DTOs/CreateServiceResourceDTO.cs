namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record CreateServiceResourceDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}