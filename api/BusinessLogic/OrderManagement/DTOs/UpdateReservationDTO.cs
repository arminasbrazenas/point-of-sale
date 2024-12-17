using PointOfSale.Models.OrderManagement.ValueObjects;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public class UpdateReservationDTO
{
    public int? EmployeeId { get; init; }
    public int? ServiceId { get; init; }
    public DateTimeOffset? StartDate { get; init; }
    public ReservationCustomer? Customer { get; init; }
    public int? BusinessId { get; init; }
}