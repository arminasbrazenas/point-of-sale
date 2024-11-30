namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record UpdateOrderDTO
{
    public List<CreateOrUpdateOrderItemDTO>? OrderItems { get; init; }
}
