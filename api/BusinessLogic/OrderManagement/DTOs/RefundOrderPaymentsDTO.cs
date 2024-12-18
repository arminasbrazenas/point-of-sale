namespace PointOfSale.BusinessLogic.OrderManagement.DTOs;

public record RefundOrderPaymentsDTO
{
    public required int OrderId { get; init; }
}