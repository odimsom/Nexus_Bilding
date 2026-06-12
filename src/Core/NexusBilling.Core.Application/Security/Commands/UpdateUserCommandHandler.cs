using MediatR;
using NexusBilling.Core.Application.Security.Contracts;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;

namespace NexusBilling.Core.Application.Security.Commands;

public sealed class UpdateUserCommandHandler(
    IUserRepository userRepo,
    IUserGroupMemberRepository memberRepo,
    IPasswordHasher hasher,
    IUnitOfWork uow)
    : IRequestHandler<UpdateUserCommand, bool>
{
    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepo.GetByIdForTenantAsync(request.TenantId, request.UserId, cancellationToken);
        if (user is null) return false;

        user.UpdateProfile(request.FullName, request.Email, request.EmployeeNo);

        if (!string.IsNullOrEmpty(request.NewPassword))
            user.ChangePassword(hasher.Hash(request.NewPassword));

        await userRepo.UpdateAsync(user, cancellationToken);

        // Re-assign group
        await memberRepo.DeleteForUserAsync(request.TenantId, user.Id, cancellationToken);
        if (!string.IsNullOrEmpty(request.GroupCode))
        {
            var member = UserGroupMember.Create(TenantIdentifier.Create(request.TenantId), request.GroupCode, user.Id);
            await memberRepo.AddAsync(member, cancellationToken);
        }

        await uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}

public sealed class ToggleUserActiveCommandHandler(
    IUserRepository userRepo,
    IUnitOfWork uow)
    : IRequestHandler<ToggleUserActiveCommand, bool>
{
    public async Task<bool> Handle(ToggleUserActiveCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepo.GetByIdForTenantAsync(request.TenantId, request.UserId, cancellationToken);
        if (user is null) return false;

        if (request.Active) user.Activate(); else user.Deactivate();
        await userRepo.UpdateAsync(user, cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
