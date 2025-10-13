
using Smart_Parking_System.Application.Interfaces;

namespace Smart_Parking_System.Infrastructure_Layer.Services
{
    public class ReservationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReservationBackgroundService> _logger;

        public ReservationBackgroundService(IServiceProvider serviceProvider , ILogger<ReservationBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var reservationRepository = scope.ServiceProvider.GetRequiredService<IReservationRepository>();

                    try
                    {
                        await reservationRepository.AutoCompleteReservationsAsync();
                        _logger.LogInformation("Auto-completed reservations at: {time}", DateTimeOffset.Now);
                        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Run every 1 minute
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while auto-completing reservations.");
                    }
                }
            }

        }

    }
}
