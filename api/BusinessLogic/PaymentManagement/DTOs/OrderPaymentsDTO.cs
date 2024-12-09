namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record OrderPaymentsDTO
{
    public required List<PaymentDTO> Payments { get; init; }
    public required decimal TotalAmount { get; init; }
    public required decimal PaidAmount { get; init; }
    public required decimal UnpaidAmount { get; init; }
}
