using PointOfSale.Models.ServiceManagement.Enums;

namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record CreateReservationDTO
{
    public required DateTime DateStart { get; init; }
    public required int ServiceId { get; init; }
    public required int EmployeeId { get; init; }
}