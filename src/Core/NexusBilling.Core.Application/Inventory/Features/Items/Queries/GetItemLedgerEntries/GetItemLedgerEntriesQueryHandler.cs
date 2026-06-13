using MediatR;
using NexusBilling.Core.Domain.Inventory.Entities;
using NexusBilling.Core.Domain.Inventory.Repositories;

namespace NexusBilling.Core.Application.Inventory.Features.Items.Queries.GetItemLedgerEntries;

public sealed class GetItemLedgerEntriesQueryHandler(IItemLedgerEntryRepository repo)
    : IRequestHandler<GetItemLedgerEntriesQuery, GetItemLedgerEntriesResult>
{
    private static readonly string[] EntryTypeLabels =
        ["Compra", "Venta", "Ajuste Positivo", "Ajuste Negativo", "Transferencia", "Consumo", "Producción"];

    public async Task<GetItemLedgerEntriesResult> Handle(GetItemLedgerEntriesQuery request, CancellationToken cancellationToken)
    {
        var items = await repo.GetForItemAsync(request.TenantId, request.ItemNo, request.Page, request.PageSize, cancellationToken);
        var total = await repo.GetTotalForItemAsync(request.TenantId, request.ItemNo, cancellationToken);

        var dtos = items.Select(e => new ItemLedgerEntryDto(
            e.EntryNo,
            e.PostingDate?.ToString("yyyy-MM-dd") ?? string.Empty,
            e.EntryType < EntryTypeLabels.Length ? EntryTypeLabels[e.EntryType] : e.EntryType.ToString(),
            e.EntryType,
            e.DocumentNo,
            e.Description,
            e.Quantity,
            e.RemainingQuantity,
            e.UnitOfMeasureCode,
            e.Positive)).ToList();

        return new GetItemLedgerEntriesResult(dtos, total, request.Page, request.PageSize);
    }
}
