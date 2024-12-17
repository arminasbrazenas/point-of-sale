using PointOfSale.Models.OrderManagement.Enums;
using PointOfSale.Models.OrderManagement.ValueObjects;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record ReservationDTO
{
    public required int Id { get; init; }
    public required ReservationDate Date { get; init; }
    public required ReservationCustomer Customer { get; init; }
    public required int EmployeeId { get; init; }
    public int? ServiceId { get; init; }
    public required int BusinessId { get; init; }
    public ReservationStatus Status { get; init; }
}