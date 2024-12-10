namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record UpdateServiceAvailabilityDTO
{
    public required int ServiceId { get; init; }
    public required int ServiceResourceId { get; init; }
    public required int Priority { get; init; }
}