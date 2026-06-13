using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Inventory.Repositories;

namespace NexusBilling.Core.Application.Inventory.Features.Items.Commands.AdjustInventory;

public sealed class AdjustInventoryCommandHandler(
    IItemLedgerEntryRepository ledgerRepo,
    NoSeriesService noSeriesService,
    IUnitOfWork uow)
    : IRequestHandler<AdjustInventoryCommand, int>
{
    public async Task<int> Handle(AdjustInventoryCommand request, CancellationToken cancellationToken)
    {
        var entryNo = await ledgerRepo.GetNextEntryNoAsync(request.TenantId, cancellationToken);
        var tenantId = TenantIdentifier.Create(request.TenantId);

        // REGLA: El número debe ser secuencial impuesto por el sistema obligatoriamente.
        string documentNo = await noSeriesService.GetNextNoAsync(request.TenantId, "AJ", cancellationToken);

        var entry = ItemLedgerEntry.CreateAdjustment(
            tenantId,
            entryNo,
            request.ItemNo,
            request.Quantity,
            documentNo,
            request.Description,
            request.UnitOfMeasureCode);

        await ledgerRepo.AddAsync(entry, cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);
        return entryNo;
    }
}
