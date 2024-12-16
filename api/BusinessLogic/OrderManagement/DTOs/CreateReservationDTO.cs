using PointOfSale.Models.OrderManagement.ValueObjects;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateReservationDTO
{
    public required int EmployeeId { get; init; }
    public required int ServiceId { get; init; }
    public required DateTimeOffset StartDate { get; init; }
    public required ReservationCustomer Customer { get; init; }
    public required int BusinessId { get; init; }
}