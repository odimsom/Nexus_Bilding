using MediatR;
using NexusBilling.Core.Application.Security.Contracts;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Security.Entities;
using NexusBilling.Core.Domain.Security.Repositories;

namespace NexusBilling.Core.Application.Security.Commands;

public sealed class CreateUserCommandHandler(
    IUserRepository userRepo,
    IUserGroupMemberRepository memberRepo,
    IPasswordHasher hasher,
    IUnitOfWork uow)
    : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var tenantId = TenantIdentifier.Create(request.TenantId);
        var hash = hasher.Hash(request.Password);

        var result = User.Create(request.Username, request.Email, hash, tenantId, request.FullName, request.EmployeeNo);
        if (!result.IsSuccess)
            throw new InvalidOperationException(result.GetError().Message);

        var user = result.GetValue();
        await userRepo.AddAsync(user, cancellationToken);

        if (!string.IsNullOrEmpty(request.GroupCode))
        {
            var member = UserGroupMember.Create(tenantId, request.GroupCode, user.Id);
            await memberRepo.AddAsync(member, cancellationToken);
        }

        await uow.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}
