namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record CreateServiceAvailabilityDTO
{
    public required int Id { get; init; }
    public required int ServiceId { get; init; }
    public required int ServiceResourceId { get; init; }
    public required int Priority { get; init; }
}