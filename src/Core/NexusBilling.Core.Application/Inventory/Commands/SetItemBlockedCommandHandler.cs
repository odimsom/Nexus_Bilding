using MediatR;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Inventory.Repositories;

namespace NexusBilling.Core.Application.Inventory.Commands;

public sealed class SetItemBlockedCommandHandler(IItemRepository repo, IUnitOfWork uow)
    : IRequestHandler<SetItemBlockedCommand, bool>
{
    public async Task<bool> Handle(SetItemBlockedCommand cmd, CancellationToken ct)
    {
        var item = await repo.GetByNoForTenantAsync(cmd.TenantId, cmd.No, ct);
        if (item is null) return false;

        if (cmd.Blocked) item.Block();
        else item.Unblock();

        await repo.UpdateAsync(item, ct);
        await uow.SaveChangesAsync(ct);
        return true;
    }
}
