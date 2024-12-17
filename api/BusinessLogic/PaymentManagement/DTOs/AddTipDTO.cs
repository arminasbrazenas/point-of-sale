namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record AddTipDTO
{
    public required int OrderId { get; init; }
    public required decimal TipAmount { get; init; }
    public required int EmployeeId { get; init; }
}
