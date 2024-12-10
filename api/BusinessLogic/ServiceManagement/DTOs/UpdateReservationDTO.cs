using PointOfSale.Models.ServiceManagement.Enums;

namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record UpdateReservationDTO
{
    public required DateTime DateStart { get; init; }
    public required DateTime DateEnd { get; init; }
    public required TimeSpan Duration { get; init; }
    public required ReservationStatus Status { get; init; }
    public required int ContactInfoId { get; init; }
    public required int ServiceId { get; init; }
    public required int EmployeeId { get; init; }
    public required DateTime LastUpdated { get; init; }
}