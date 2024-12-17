namespace PointOfSale.Models.OrderManagement.ValueObjects;

public record ReservationDate
{
    public required DateTimeOffset Start { get; set; }
    public required DateTimeOffset End { get; set; }
}
