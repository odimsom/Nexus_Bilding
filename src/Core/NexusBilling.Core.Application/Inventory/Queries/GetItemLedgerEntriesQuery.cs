using MediatR;

namespace NexusBilling.Core.Application.Inventory.Queries;

public record ItemLedgerEntryDto(
    int EntryNo,
    string PostingDate,
    string EntryTypeLabel,
    short EntryType,
    string DocumentNo,
    string Description,
    decimal Quantity,
    decimal RemainingQuantity,
    string UnitOfMeasureCode,
    bool Positive);

public record GetItemLedgerEntriesQuery(
    Guid TenantId,
    string ItemNo,
    int Page,
    int PageSize) : IRequest<GetItemLedgerEntriesResult>;

public record GetItemLedgerEntriesResult(
    IReadOnlyList<ItemLedgerEntryDto> Items,
    int TotalCount,
    int Page,
    int PageSize);
