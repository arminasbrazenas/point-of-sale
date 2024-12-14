namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record UpdateServiceAvailabilityDTO
{
    public int? ServiceId { get; init; }
    public int? ServiceResourceId { get; init; }
    public int? Priority { get; init; }
}