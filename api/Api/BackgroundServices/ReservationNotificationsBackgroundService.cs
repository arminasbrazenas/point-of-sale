using PointOfSale.BusinessLogic.OrderManagement.Interfaces;

namespace PointOfSale.Api.BackgroundServices;

public class ReservationNotificationsBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ReservationNotificationsBackgroundService> _logger;

    public ReservationNotificationsBackgroundService(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<ReservationNotificationsBackgroundService> logger
    )
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
        var reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await reservationService.SendUnsentNotifications();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
