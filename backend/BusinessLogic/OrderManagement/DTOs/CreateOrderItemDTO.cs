namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateOrderItemDTO
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}
