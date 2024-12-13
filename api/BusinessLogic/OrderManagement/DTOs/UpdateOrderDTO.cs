namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record UpdateOrderDTO
{
    public List<CreateOrUpdateOrderItemDTO>? OrderItems { get; init; }
    public List<int>? ServiceChargeIds { get; init; }
    public required List<CreateOrderDiscountDTO>? Discounts { get; init; }
}
