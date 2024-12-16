namespace PointOfSale.Models.OrderManagement.ValueObjects;

public record ReservationCustomer
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}