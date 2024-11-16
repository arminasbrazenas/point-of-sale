using PointOfSale.Models.OrderManagement.Enums;

namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record OrderMinimalDTO
{
    public required int Id { get; init; }
    public required OrderStatus Status { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}
