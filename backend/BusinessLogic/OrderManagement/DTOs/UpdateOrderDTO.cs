namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record UpdateOrderDTO
{
    public required List<CreateOrderItemDTO>? OrderItems { get; init; }
}
