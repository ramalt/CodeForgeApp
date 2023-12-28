using CodeForge.Common;
using CodeForge.Common.Events.Entry;
using CodeForge.Common.Infrastructure;

namespace CodeForge.Projections.FavoriteService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;

    public Worker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string? connectionString = _configuration.GetConnectionString("SqlServer");

        Services.FavoriteService favService = new(connectionString);

        QueueFactory.CreateBasicConsumer()
                    .EnsureExchange(AppConstants.FAV_EXCHANGE_NAME)
                    .EnsureQueue(AppConstants.CREATE_ENTRY_FAV_QUEUE_NAME, AppConstants.FAV_EXCHANGE_NAME)
                    .Receive<CreateEntryFavEvent>(async fav =>
                    {
                        
                        await favService.CreateEntryFav(fav);

                        _logger.LogInformation($"Received EntryId '{fav.EntryId}'");
                    })
                    .startConsuming(AppConstants.CREATE_ENTRY_FAV_QUEUE_NAME);
    }
}
