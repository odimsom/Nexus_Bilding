using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusBilling.Api.Common;
using NexusBilling.Core.Application.Security.Commands;
using NexusBilling.Core.Application.Security.Queries;

namespace NexusBilling.Api.Controllers.Security;

public record CreateUserRequest(
    string Username,
    string Email,
    string Password,
    string FullName,
    string EmployeeNo,
    string GroupCode);

public record UpdateUserRequest(
    string FullName,
    string Email,
    string EmployeeNo,
    string GroupCode,
    string? NewPassword);

[Authorize]
[ApiController]
[Route("api/v1/security")]
public sealed class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("users")]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var result = await mediator.Send(new GetUsersQuery(GetTenantId()), ct);
        return Ok(ApiResponse<IReadOnlyList<UserDto>>.Ok(result));
    }

    [HttpGet("groups")]
    public async Task<IActionResult> ListGroups(CancellationToken ct)
    {
        var result = await mediator.Send(new GetUserGroupsQuery(GetTenantId()), ct);
        return Ok(ApiResponse<IReadOnlyList<UserGroupDto>>.Ok(result));
    }

    [HttpPost("users")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest req, CancellationToken ct)
    {
        var id = await mediator.Send(new CreateUserCommand(
            GetTenantId(), req.Username, req.Email, req.Password,
            req.FullName, req.EmployeeNo, req.GroupCode), ct);
        return Ok(ApiResponse<object>.Ok(new { id }));
    }

    [HttpPut("users/{userId:guid}")]
    public async Task<IActionResult> Update(Guid userId, [FromBody] UpdateUserRequest req, CancellationToken ct)
    {
        var ok = await mediator.Send(new UpdateUserCommand(
            GetTenantId(), userId, req.FullName, req.Email,
            req.EmployeeNo, req.GroupCode, req.NewPassword), ct);
        return ok ? Ok(ApiResponse<bool>.Ok(true)) : NotFound();
    }

    [HttpPatch("users/{userId:guid}/activate")]
    public async Task<IActionResult> Activate(Guid userId, CancellationToken ct)
    {
        var ok = await mediator.Send(new ToggleUserActiveCommand(GetTenantId(), userId, true), ct);
        return ok ? Ok(ApiResponse<bool>.Ok(true)) : NotFound();
    }

    [HttpPatch("users/{userId:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid userId, CancellationToken ct)
    {
        var ok = await mediator.Send(new ToggleUserActiveCommand(GetTenantId(), userId, false), ct);
        return ok ? Ok(ApiResponse<bool>.Ok(true)) : NotFound();
    }

    private Guid GetTenantId()
    {
        var claim = User.FindFirst("tenant_id")?.Value;
        return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
    }
}
