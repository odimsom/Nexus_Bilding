using MediatR;

namespace NexusBilling.Core.Application.Security.Features.Users.Queries.GetUserGroups;

public record UserGroupDto(string Code, string Name, bool AssignToAllNewUsers);
public record GetUserGroupsQuery(Guid TenantId) : IRequest<IReadOnlyList<UserGroupDto>>;
