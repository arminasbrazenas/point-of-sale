namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record CreateServiceResourceDTO
{
    public required string Name { get; init; }
}