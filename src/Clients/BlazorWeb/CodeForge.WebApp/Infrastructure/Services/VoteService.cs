using CodeForge.Common.ViewModels;
using CodeForge.WebApp.Infrastructure.Services.Interfaces;


namespace CodeForge.WebApp.Infrastructure.Services;

public class VoteService : IVoteService
{
    private readonly HttpClient _client;

    public VoteService(HttpClient client)
    {
        _client = client;
    }

    public async Task DeleteEntryVote(Guid entryId, CancellationToken cancellationToken)
    {
        await _client.DeleteAsync($"/api/vote/entry/{entryId}", cancellationToken);
    }

    public async Task DeleteEntryCommentVote(Guid entryCommentId, CancellationToken cancellationToken)
    {
        await _client.DeleteAsync($"/api/vote/entry/comment/{entryCommentId}", cancellationToken);
    }

    public async Task CreateEntryUpVote(Guid entryId, CancellationToken cancellationToken)
    {
        await CreateEntryVote(entryId, cancellationToken, VoteType.DownVote);
    }

    public async Task CreateEntryDownVote(Guid entryId, CancellationToken cancellationToken)
    {
        await CreateEntryVote(entryId, cancellationToken, VoteType.DownVote);
    }

    public async Task CreateEntryCommentUpVote(Guid entryCommentId, CancellationToken cancellationToken)
    {
        await CreateEntryCommentVote(entryCommentId, cancellationToken, VoteType.DownVote);
    }

    public async Task CreateEntryCommentDownVote(Guid entryCommentId, CancellationToken cancellationToken)
    {
        await CreateEntryCommentVote(entryCommentId, cancellationToken, VoteType.DownVote);
    }

    #region privates

    private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, CancellationToken cancellationToken, VoteType vote = VoteType.UpVote)
    {
        var res = await _client.PostAsync($"/api/vote/entry/{entryId}?votetype={vote}", null, cancellationToken: cancellationToken);
        return res;
    }

    private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, CancellationToken cancellationToken, VoteType vote = VoteType.UpVote)
    {
        var res = await _client.PostAsync($"/api/vote/entry/comment/{entryCommentId}?votetype={vote}", null, cancellationToken: cancellationToken);
        return res;
    }

    #endregion

}
