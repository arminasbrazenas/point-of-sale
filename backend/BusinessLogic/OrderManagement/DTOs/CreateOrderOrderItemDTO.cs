namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateOrderOrderItemDTO
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
}