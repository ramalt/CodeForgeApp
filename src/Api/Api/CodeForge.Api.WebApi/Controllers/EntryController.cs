using CodeForge.Api.Application.Features.Queries.GetEntries;
using CodeForge.Common.ViewModels.Queries;
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
    [HttpGet]
    public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
    {
        var res = await _sender.Send(query);
        return Ok(res);
    }

    [HttpGet]
    [Route("subjects")]
    public async Task<IActionResult> GetEntrySubjects(int page, int pageSize)
    {
        var res = await _sender.Send(new GetMainPageEntriesQuery(pageSize: pageSize, page: page, userId: UserId));
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
    {
        if (!command.OwnerId.HasValue)
            command.OwnerId = UserId;

        var res = await _sender.Send(command);
        return Ok(res);
    }

    [HttpPost]
    [Route("comment")]
    public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
    {
        if (!command.OwnerId.HasValue)
            command.OwnerId = UserId;

        var res = await _sender.Send(command);
        return Ok(res);
    }
}

