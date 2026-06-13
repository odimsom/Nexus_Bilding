using MediatR;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.ReleaseSalesOrder;

public sealed class ReleaseSalesOrderCommandHandler(
    ISalesHeaderRepository repo,
    IUnitOfWork uow)
    : IRequestHandler<ReleaseSalesOrderCommand, bool>
{
    public async Task<bool> Handle(ReleaseSalesOrderCommand cmd, CancellationToken ct)
    {
        var header = await repo.GetByNoForTenantAsync(cmd.TenantId, cmd.No, ct);
        if (header is null) return false;

        header.Release();
        await repo.UpdateAsync(header, ct);
        await uow.SaveChangesAsync(ct);
        return true;
    }
}
