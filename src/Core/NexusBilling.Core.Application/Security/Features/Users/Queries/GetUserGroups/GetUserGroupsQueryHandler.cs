using MediatR;
using NexusBilling.Core.Domain.Security.Repositories;

namespace NexusBilling.Core.Application.Security.Features.Users.Queries.GetUserGroups;

public sealed class GetUserGroupsQueryHandler(IUserGroupRepository repo)
    : IRequestHandler<GetUserGroupsQuery, IReadOnlyList<UserGroupDto>>
{
    public async Task<IReadOnlyList<UserGroupDto>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await repo.GetAllForTenantAsync(request.TenantId, cancellationToken);
        return groups.Select(g => new UserGroupDto(g.Code, g.Name, g.AssignToAllNewUsers)).ToList();
    }
}
