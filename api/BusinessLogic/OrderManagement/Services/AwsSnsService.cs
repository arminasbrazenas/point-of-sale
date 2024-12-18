using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Logging;
using PointOfSale.BusinessLogic.OrderManagement.Interfaces;

namespace PointOfSale.BusinessLogic.OrderManagement.Services;

public class AwsSnsService : ISmsMessageService
{
    private readonly AmazonSimpleNotificationServiceClient _snsClient;
    private readonly ILogger<AwsSnsService> _logger;

    public AwsSnsService(AmazonSimpleNotificationServiceClient snsClient, ILogger<AwsSnsService> logger)
    {
        _snsClient = snsClient;
        _logger = logger;
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
            _logger.LogError(ex.Message);
        }
    }
}
