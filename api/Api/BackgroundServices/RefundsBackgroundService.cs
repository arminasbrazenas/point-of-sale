using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

namespace PointOfSale.Api.BackgroundServices;

public class RefundsBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<RefundsBackgroundService> _logger;

    public RefundsBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<RefundsBackgroundService> logger)
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
                await paymentService.CompletePendingRefunds();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
