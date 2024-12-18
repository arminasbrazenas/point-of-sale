namespace PointOfSale.Models.OrderManagement.ValueObjects;

public record ReservationNotification
{
    public required Guid IdempotencyKey { get; init; }
    public required DateTimeOffset? SentAt { get; init; }
}
