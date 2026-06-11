using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Core.Application.Security.Queries;

namespace NexusBilling.Api.Controllers.Security;

[Authorize]
[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("{userId}/setup")]
    public async Task<IActionResult> GetUserSetup(string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserSetupQuery(userId);
        var result = await _mediator.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { error = result.GetError().Message });

        return Ok(result.GetValue());
    }
}
