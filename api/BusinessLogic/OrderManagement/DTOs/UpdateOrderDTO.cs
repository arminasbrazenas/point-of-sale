namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record UpdateOrderDTO
{
    public List<CreateOrderItemDTO>? OrderItems { get; init; }
}
