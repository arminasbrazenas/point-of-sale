using PointOfSale.BusinessLogic.OrderManagement.Interfaces;

namespace PointOfSale.Api.BackgroundServices;

public class ReservationNotificationsBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ReservationNotificationsBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await Run(stoppingToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task Run(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await reservationService.SendUnsentNotifications();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
