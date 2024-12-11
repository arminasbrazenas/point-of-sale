namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateOrderDTO
{
    public required List<CreateOrUpdateOrderItemDTO> OrderItems { get; init; }
    public required List<int> ServiceChargeIds { get; set; }
    public required int BusinessId { get; init; }
}
