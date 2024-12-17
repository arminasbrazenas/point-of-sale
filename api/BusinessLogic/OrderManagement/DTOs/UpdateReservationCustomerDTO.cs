namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record UpdateReservationCustomerDTO
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}
