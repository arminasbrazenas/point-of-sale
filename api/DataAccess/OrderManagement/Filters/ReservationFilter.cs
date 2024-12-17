using PointOfSale.Models.OrderManagement.Enums;

namespace PointOfSale.DataAccess.OrderManagement.Filters;

public record ReservationFilter
{
    public ReservationStatus? Status { get; init; }
}
