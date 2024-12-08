using PointOfSale.Models.PaymentManagement.Enums;

namespace PointOfSale.BusinessLogic.PaymentManagement.DTOs;

public record PaymentDTO
{
    public required int Id { get; init; }
    public required decimal Amount { get; init; }
    public required PaymentMethod Method { get; set; }
}
