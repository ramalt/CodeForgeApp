using CodeForge.Common.ViewModels.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeForge.Api.WebApi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("login")]

    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var res = await _sender.Send(command);
        return Ok(res);
    }

    [HttpPost]
    [Route("register")]

    public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
    {
        var res = await _sender.Send(command);
        return Ok(res);
    }

    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        var res = await _sender.Send(command);
        return Ok(res);
    }
}
