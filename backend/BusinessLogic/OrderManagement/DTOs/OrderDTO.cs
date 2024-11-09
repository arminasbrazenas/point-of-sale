namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record OrderDTO : OrderMinimalDTO
{
    public required List<OrderItemDTO> OrderItems { get; init; }
    public required decimal TotalPrice { get; init; }
}
