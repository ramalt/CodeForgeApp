using CodeForge.Common;
using CodeForge.Common.Events.Entry;
using CodeForge.Common.Events.EntryComment;
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

        QueueFactory.CreateBasicConsumer()
                   .EnsureExchange(AppConstants.FAV_EXCHANGE_NAME)
                   .EnsureQueue(AppConstants.DELETE_ENTRY_FAV_QUEUE_NAME, AppConstants.FAV_EXCHANGE_NAME)
                   .Receive<DeleteEntryFavEvent>(async fav =>
                   {

                       await favService.DeleteEntryFav(fav);

                       _logger.LogInformation($"Received EntryId '{fav.EntryId}'");
                   })
                   .startConsuming(AppConstants.DELETE_ENTRY_FAV_QUEUE_NAME);

        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(AppConstants.FAV_EXCHANGE_NAME)
            .EnsureQueue(AppConstants.CREATE_COMMENT_FAV_QUEUE_NAME, AppConstants.FAV_EXCHANGE_NAME)
            .Receive<CreateEntryCommentFavEvent>(async fav =>
            {

                await favService.CreateEntryCommentFav(fav);

                _logger.LogInformation($"Received EntryId '{fav.EntryCommentId}'");
            })
            .startConsuming(AppConstants.CREATE_COMMENT_FAV_QUEUE_NAME);

        QueueFactory.CreateBasicConsumer()
                   .EnsureExchange(AppConstants.FAV_EXCHANGE_NAME)
                   .EnsureQueue(AppConstants.DELETE_COMMENT_FAV_QUEUE_NAME, AppConstants.FAV_EXCHANGE_NAME)
                   .Receive<DeleteEntryCommentFavEvent>(async fav =>
                   {

                       await favService.DeleteEntryCommentFav(fav);

                       _logger.LogInformation($"Received EntryId '{fav.Id}'");
                   })
                   .startConsuming(AppConstants.DELETE_COMMENT_FAV_QUEUE_NAME);
    }

}
