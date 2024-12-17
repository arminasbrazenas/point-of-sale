namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public class UpdateReservationDTO
{
    public int? EmployeeId { get; init; }
    public int? ServiceId { get; init; }
    public DateTimeOffset? StartDate { get; init; }
    public required UpdateReservationCustomerDTO Customer { get; init; }
}
