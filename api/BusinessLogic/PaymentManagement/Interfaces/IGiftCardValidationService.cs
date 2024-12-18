namespace PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

public interface IGiftCardValidationService
{
    Task<string> ValidateCode(string code, int businessId);
    decimal ValidateAmount(decimal amount);
    DateTimeOffset ValidateExpiration(DateTimeOffset expiresAt);
}
