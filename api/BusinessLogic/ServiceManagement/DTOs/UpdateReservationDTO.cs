using PointOfSale.Models.ServiceManagement.Enums;

namespace PointOfSale.BusinessLogic.ServiceManagement.DTOs;

public record UpdateReservationDTO
{
    public DateTime? DateStart { get; init; }
    public int? ContactInfoId { get; init; }
    public int? ServiceId { get; init; }
    public int? EmployeeId { get; init; }
}