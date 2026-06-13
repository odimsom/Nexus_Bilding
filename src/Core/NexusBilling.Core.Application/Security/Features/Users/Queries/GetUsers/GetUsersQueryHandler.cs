using MediatR;
using NexusBilling.Core.Domain.Security.Repositories;

namespace NexusBilling.Core.Application.Security.Features.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler(
    IUserRepository userRepo,
    IUserGroupMemberRepository memberRepo,
    IUserGroupRepository groupRepo)
    : IRequestHandler<GetUsersQuery, IReadOnlyList<UserDto>>
{
    public async Task<IReadOnlyList<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users   = await userRepo.GetAllForTenantAsync(request.TenantId, cancellationToken);
        var groups  = await groupRepo.GetAllForTenantAsync(request.TenantId, cancellationToken);
        var groupMap = groups.ToDictionary(g => g.Code, g => g.Name);

        var result = new List<UserDto>();
        foreach (var u in users)
        {
            var members   = await memberRepo.GetForUserAsync(request.TenantId, u.Id, cancellationToken);
            var groupCode = members.FirstOrDefault()?.UserGroupCode ?? string.Empty;
            var groupName = groupMap.TryGetValue(groupCode, out var n) ? n : string.Empty;
            result.Add(new UserDto(u.Id, u.Username, u.Email, u.FullName, u.EmployeeNo, u.IsActive, groupCode, groupName));
        }
        return result;
    }
}
