using CodeForge.Common.ViewModels;
using MediatR;

namespace CodeForge.Api.Application.Features.Commands.Entry.DeleteVote;

public class DeleteEntryVoteCommand(Guid entryId, Guid createdBy) : IRequest<bool>
{
    public Guid EntryId { get; set; }
    public Guid CreatedBy { get; set; }
    public VoteType Vote { get; set; }
}
