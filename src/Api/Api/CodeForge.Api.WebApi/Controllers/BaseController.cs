using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace CodeForge.Api.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    public Guid? UserId
    {
        get
        {
            var val = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return val is null ? null : new Guid(val);
        }
    }

}
