namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateOrderDTO
{
    public required List<CreateOrderItemDTO> OrderItems { get; init; }
}
