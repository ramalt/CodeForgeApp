namespace CodeForge.WebApp.Infrastructure.Services.Interfaces;

public interface IVoteService
{
    Task DeleteEntryVote(Guid entryId, CancellationToken cancellationToken);
    Task DeleteEntryCommentVote(Guid entryCommentId, CancellationToken cancellationToken);
    Task CreateEntryUpVote(Guid entryId, CancellationToken cancellationToken);
    Task CreateEntryDownVote(Guid entryId, CancellationToken cancellationToken);
    Task CreateEntryCommentUpVote(Guid entryCommentId, CancellationToken cancellationToken);
    Task CreateEntryCommentDownVote(Guid entryCommentId, CancellationToken cancellationToken);

}
