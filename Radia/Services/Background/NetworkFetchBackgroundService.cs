using Radia.Services.FileProviders;

namespace Radia.Services.Background
{
    public class NetworkFetchBackgroundService : BackgroundService
    {
        private readonly ILogger<NetworkFetchBackgroundService> _logger;
        private Timer? timer = null;

        public NetworkFetchBackgroundService(IServiceProvider services,
            ILogger<NetworkFetchBackgroundService> logger)
        {
            Services = services;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Started fetching network.");

            this.timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            _logger.LogInformation("Fetching Network changes...");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IRadiaNetworkFileProvider>();

                scopedProcessingService.Fetch();
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Done fetching network.");

            this.timer?.Change(Timeout.Infinite, 0);

            await base.StopAsync(stoppingToken);
        }

        public override void Dispose()
        {
            this.timer?.Dispose();

            base.Dispose();
        }
    }
}
