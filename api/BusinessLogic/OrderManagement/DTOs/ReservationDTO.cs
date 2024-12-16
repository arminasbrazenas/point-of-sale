using PointOfSale.Models.OrderManagement.ValueObjects;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record ReservationDTO
{
    // TODO: add more properties
    public required ReservationDate StartDate { get; init; }
    public required ReservationCustomer Customer { get; init; }
}