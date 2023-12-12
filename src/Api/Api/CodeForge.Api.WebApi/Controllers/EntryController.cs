using CodeForge.Common.ViewModels.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeForge.Api.WebApi.Controllers;

public class EntryController : BaseController
{
    private readonly ISender _sender;

    public EntryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("entry")]
    public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand entry)
    {
        if (!entry.OwnerId.HasValue)
            entry.OwnerId = UserId;

        var res = await _sender.Send(entry);
        return Ok(res);
    }

    [HttpPost]
    [Route("entry/comment")]
    public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand entry)
    {
        if (!entry.OwnerId.HasValue)
            entry.OwnerId = UserId;

        var res = await _sender.Send(entry);
        return Ok(res);
    }
}

