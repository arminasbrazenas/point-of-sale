namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record OrderReceiptDTO
{
    public required decimal TotalPrice { get; init; }
    public required List<OrderItemDTO> OrderItems { get; init; }
    public required List<OrderDiscountDTO> Discounts { get; init; }
    public required List<OrderServiceChargeDTO> ServiceCharges { get; init; }
}
