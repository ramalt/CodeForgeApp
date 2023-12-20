using CodeForge.Api.Application.Features.Queries.GetEntries;
using CodeForge.Api.Application.Features.Queries.GetEntryComments;
using CodeForge.Api.Application.Features.Queries.GetEntryDetail;
using CodeForge.Api.Application.Features.Queries.GetUserEntries;
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
    [Route("subject")]
    public async Task<IActionResult> GetEntrySubjects(int page, int pageSize)
    {
        var res = await _sender.Send(new GetMainPageEntriesQuery(pageSize: pageSize, page: page, userId: UserId));
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _sender.Send(new GetEntryDetailQuery(id, UserId));
        return Ok(result);
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

    [HttpGet]
    [Route("comment/{id}")]
    public async Task<IActionResult> GetEntryComments(Guid id, int page, int pageSize)
    {
        var result = await _sender.Send(new GetEntryCommentsQuery(id, UserId, page, pageSize));

        return Ok(result);
    }

    [HttpGet]
    [Route("user/comment")]
    public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
    {
        if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
            userId = UserId.Value;

        var result = await _sender.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));

        return Ok(result);
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
    {
        var result = await _sender.Send(query);

        return Ok(result);
    }




}

