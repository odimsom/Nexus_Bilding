using MediatR;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Commands;

public sealed class SetCustomerBlockedCommandHandler(ICustomerRepository repo, IUnitOfWork uow)
    : IRequestHandler<SetCustomerBlockedCommand, bool>
{
    public async Task<bool> Handle(SetCustomerBlockedCommand cmd, CancellationToken ct)
    {
        var customer = await repo.GetByNoForTenantAsync(cmd.TenantId, cmd.No, ct);
        if (customer is null) return false;

        if (cmd.Blocked) customer.Block();
        else customer.Unblock();

        await repo.UpdateAsync(customer, ct);
        await uow.SaveChangesAsync(ct);
        return true;
    }
}
