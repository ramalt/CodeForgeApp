using CodeForge.Api.Application.Features.Commands.Entry.DeleteVote;
using CodeForge.Api.Application.Features.Commands.EntryComment.DeleteVote;
using CodeForge.Common.ViewModels;
using CodeForge.Common.ViewModels.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeForge.Api.WebApi.Controllers;

public class VoteController : BaseController
{
    private readonly ISender _sender;

    public VoteController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("entry/{entryId}")]
    public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
    {
        var result = await _sender.Send(new CreateEntryVoteCommand(voteType, entryId, UserId.Value));

        return Ok(result);
    }

    [HttpPost]
    [Route("entry/comment/{entryCommentId}")]
    public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
    {
        var result = await _sender.Send(new CreateEntryCommentVoteCommand(entryCommentId, voteType, UserId.Value));

        return Ok(result);
    }

    [HttpDelete]
    [Route("entry/{entryId}")]
    public async Task<IActionResult> DeleteEntryVote(Guid entryId)
    {
        await _sender.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));

        return Ok();
    }

    [HttpDelete]
    [Route("entry/comment/{entryId}")]
    public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
    {
        await _sender.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId.Value));

        return Ok();
    }
}