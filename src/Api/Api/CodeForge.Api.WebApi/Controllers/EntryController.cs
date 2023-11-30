using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeForge.Common.ViewModels.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodeForge.Api.WebApi.Controllers;

[ApiController]
[Route("api/entry")]
public class EntryController : ControllerBase
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
        var res = await _sender.Send(entry);
        return Ok(res);
    }

        [HttpPost]
    [Route("entry/comment")]
    public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand entry)
    {
        var res = await _sender.Send(entry);
        return Ok(res);
    }
}

