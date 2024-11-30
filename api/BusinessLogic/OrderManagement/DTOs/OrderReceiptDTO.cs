namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record OrderReceiptDTO
{
    public required decimal TotalPrice { get; init; }
    public required decimal TaxTotal { get; init; }
    public required List<OrderItemDTO> OrderItems { get; init; }
}
