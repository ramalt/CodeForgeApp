using CodeForge.Common.ViewModels;
using MediatR;

namespace CodeForge.Api.Application.Features.Commands.EntryComment.DeleteVote;

public class DeleteEntryCommentVoteCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public VoteType Vote { get; set; }
    public Guid CreatedBy { get; set; }

    public DeleteEntryCommentVoteCommand(Guid createdBy, VoteType vote, Guid id)
    {
        CreatedBy = createdBy;
        Vote = vote;
        Id = id;
    }

    public DeleteEntryCommentVoteCommand()
    {
        
    }
}
