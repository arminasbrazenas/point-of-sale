namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record PayByCashDTO
{
    public required int OrderId { get; init; }
    public required decimal PaymentAmount { get; init; }
    public required int BusinessId {get; init;}
    public required int EmployeeId {get; init;}
}
