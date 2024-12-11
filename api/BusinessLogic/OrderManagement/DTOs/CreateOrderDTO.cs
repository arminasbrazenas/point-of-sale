namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record CreateOrderDTO
{
    public required int BusinessId { get; set; }
    public required List<CreateOrUpdateOrderItemDTO> OrderItems { get; init; }
    public required List<int> ServiceChargeIds { get; set; }
}
