using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

namespace PointOfSale.Api.BackgroundServices;

public class RefundsBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public RefundsBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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
                Console.WriteLine(ex);
            }
            
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}