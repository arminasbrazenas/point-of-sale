using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class AwsSnsService : ISmsMessageService
{
    private readonly AmazonSimpleNotificationServiceClient _snsClient;

    public AwsSnsService(AmazonSimpleNotificationServiceClient snsClient)
    {
        _snsClient = snsClient;
    }

    public async Task SendSmsMessage(string phoneNumber, string message, Guid idempotencyKey)
    {
        var publishRequest = new PublishRequest
        {
            Message = message,
            PhoneNumber = phoneNumber,
            MessageDeduplicationId = idempotencyKey.ToString(),
        };

        try
        {
            await _snsClient.PublishAsync(publishRequest);
        }
        catch (InvalidParameterException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
