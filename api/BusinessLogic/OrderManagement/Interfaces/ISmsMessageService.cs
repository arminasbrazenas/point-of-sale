namespace PointOfSale.BusinessLogic.OrderManagement.Interfaces;

public interface ISmsMessageService
{
    Task SendSmsMessage(string phoneNumber, string message, Guid idempotencyKey);
}
