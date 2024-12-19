using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

namespace PointOfSale.Api.BackgroundServices;

public class PaymentBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<PaymentBackgroundService> _logger;

    public PaymentBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<PaymentBackgroundService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await Run(stoppingToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }

    private async Task Run(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await paymentService.ProcessPendingCardPayments();
                await paymentService.CancelPendingOutdatedCardPayments();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        }
    }
}
