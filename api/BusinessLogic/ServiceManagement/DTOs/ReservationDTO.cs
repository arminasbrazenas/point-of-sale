using PointOfSale.Models.ServiceManagement.Enums;

namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record ReservationDTO
{
    public required int Id { get; init; }
    public required DateTime DateStart { get; init; }
    public required DateTime DateEnd { get; init; }
    public required ReservationStatus Status { get; init; }
    public required int ServiceResourceId { get; set; }
    public required int ContactInfoId { get; init; }
    public required int ServiceId { get; init; }
    public required int EmployeeId { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime LastUpdated { get; init; }
}   