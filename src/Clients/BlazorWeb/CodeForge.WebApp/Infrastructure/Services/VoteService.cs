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

    public async Task DeleteEntryVote(Guid entryId)
    {
        await _client.DeleteAsync($"/api/vote/entry/{entryId}");
    }

    public async Task DeleteEntryCommentVote(Guid entryCommentId)
    {
        await _client.DeleteAsync($"/api/vote/entry/comment/{entryCommentId}");
    }

    public async Task CreateEntryUpVote(Guid entryId)
    {
        await CreateEntryVote(entryId, VoteType.DownVote);
    }

    public async Task CreateEntryDownVote(Guid entryId)
    {
        await CreateEntryVote(entryId, VoteType.DownVote);
    }

    public async Task CreateEntryCommentUpVote(Guid entryCommentId)
    {
        await CreateEntryCommentVote(entryCommentId, VoteType.DownVote);
    }

    public async Task CreateEntryCommentDownVote(Guid entryCommentId)
    {
        await CreateEntryCommentVote(entryCommentId, VoteType.DownVote);
    }

    #region privates

    private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType vote = VoteType.UpVote)
    {
        var res = await _client.PostAsync($"/api/vote/entry/{entryId}?votetype={vote}", null);
        return res;
    }

    private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType vote = VoteType.UpVote)
    {
        var res = await _client.PostAsync($"/api/vote/entry/comment/{entryCommentId}?votetype={vote}", null);
        return res;
    }

    #endregion

}
