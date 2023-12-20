using CodeForge.Api.Application.Features.Commands.Entry.CreateFav;
using CodeForge.Api.Application.Features.Commands.EntryComment.CreateFav;
using CodeForge.Api.Application.Features.Commands.EntryComment.DeleteFav;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeForge.Api.WebApi.Controllers;

public class FavoriteController : BaseController
{
    private readonly ISender _sender;

    public FavoriteController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("entry/{entryId}")]
    public async Task<IActionResult> CreateEntryFav(Guid entryId)
    {
        var result = await _sender.Send(new CreateEntryFavCommand(entryId, UserId.Value));

        return Ok(result);
    }

    [HttpPost]
    [Route("entrycomment/{entrycommentId}")]
    public async Task<IActionResult> CreateEntryCommentFav(Guid entrycommentId)
    {
        var result = await _sender.Send(new CreateEntryCommentFavCommand(entrycommentId, UserId.Value));

        return Ok(result);
    }


    [HttpPost]
    [Route("deleteentryfav/{entryId}")]
    public async Task<IActionResult> DeleteEntryFav(Guid entryId)
    {
        var result = await _sender.Send(new DeleteEntryFavCommand(entryId, UserId.Value));

        return Ok(result);
    }

    [HttpPost]
    [Route("deleteentrycommentfav/{entrycommentId}")]
    public async Task<IActionResult> DeleteEntryCommentFav(Guid entrycommentId)
    {
        var result = await _sender.Send(new DeleteEntryCommentFavCommand(entrycommentId, UserId.Value));

        return Ok(result);
    }
}
