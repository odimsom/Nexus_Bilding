using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class ItemAnalysisViewEntry : Entity
{
    private ItemAnalysisViewEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public short AnalysisArea { get; private set; }
    public string AnalysisViewCode { get; private set; }
    public string ItemNo { get; private set; }
    public short SourceType { get; private set; }
    public string SourceNo { get; private set; }
    public string LocationCode { get; private set; }
    public string Dimension1ValueCode { get; private set; }
    public string Dimension2ValueCode { get; private set; }
    public string Dimension3ValueCode { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public int EntryNo { get; private set; }
    public short ItemLedgerEntryType { get; private set; }
    public short EntryType { get; private set; }
    public decimal InvoicedQuantity { get; private set; }
    public decimal SalesAmountActual { get; private set; }
    public decimal CostAmountActual { get; private set; }
    public decimal CostAmountNonInvtbl { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal SalesAmountExpected { get; private set; }
    public decimal CostAmountExpected { get; private set; }

    public static OperationResult<ItemAnalysisViewEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ItemAnalysisViewEntry, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ItemAnalysisViewEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<ItemAnalysisViewEntry, DomainError>.Ok(entity);
    }
}
