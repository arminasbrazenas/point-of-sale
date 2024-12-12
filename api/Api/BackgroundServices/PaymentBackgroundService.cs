using PointOfSale.BusinessLogic.PaymentManagement.Interfaces;

namespace PointOfSale.Api.BackgroundServices;

public class PaymentBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PaymentBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // using var scope = _serviceScopeFactory.CreateScope();
        // var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     await paymentService.ProcessPendingOnlinePayments();
        //     await paymentService.CancelPendingOutdatedOnlinePayments();

        //     await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        // }
    }
}
