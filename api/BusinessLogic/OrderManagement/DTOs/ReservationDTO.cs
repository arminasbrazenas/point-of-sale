using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.OrderManagement.ValueObjects;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record ReservationDTO
{
    public required int Id { get; init; }
    public required string Description { get; init; }
    public required ReservationDate Date { get; init; }
    public required ReservationCustomer Customer { get; init; }
    public required ServiceEmployeeDTO Employee { get; init; }
    public required int? ServiceId { get; init; }
    public required ReservationStatus Status { get; init; }
    public required decimal Price { get; init; }
    public required DateTimeOffset BookedAt { get; init; }
}
