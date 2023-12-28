using CodeForge.Common;
using CodeForge.Common.Events.Entry;
using CodeForge.Common.Infrastructure;

namespace CodeForge.Projections.VoteService;

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
        string? connectionstring = _configuration.GetConnectionString("SqlServer");

        Services.VoteService voteService = new(connectionstring);

        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(AppConstants.VOTE_EXCHANGE_NAME)
            .EnsureQueue(AppConstants.CREATE_ENTRY_VOTE_QUEUE_NAME, AppConstants.VOTE_EXCHANGE_NAME)
            .Receive<CreateEntryVoteEvent>(vote =>
            {
                voteService.CreateEntryVote(vote).GetAwaiter().GetResult();
                _logger.LogInformation("Create Entry Received EntryId: {0}, VoteType: {1}", vote.EntryId, vote.Vote);
            })
            .startConsuming(AppConstants.CREATE_ENTRY_VOTE_QUEUE_NAME);

        QueueFactory.CreateBasicConsumer()
            .EnsureExchange(AppConstants.VOTE_EXCHANGE_NAME)
            .EnsureQueue(AppConstants.DELETE_ENTRY_VOTE_QUEUE_NAME, AppConstants.VOTE_EXCHANGE_NAME)
            .Receive<DeleteEntryVoteEvent>(vote =>
            {
                voteService.DeleteEntryVote(vote.EntryId, vote.CreatedBy).GetAwaiter().GetResult();
                _logger.LogInformation("Delete Entry Received EntryId: {0}", vote.EntryId);
            })
            .startConsuming(AppConstants.DELETE_ENTRY_VOTE_QUEUE_NAME);

    }
}
