using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class IntrastatJnlLine : Entity
{
    private IntrastatJnlLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string JournalTemplateName { get; private set; }
    public string JournalBatchName { get; private set; }
    public int LineNo { get; private set; }
    public short Type { get; private set; }
    public DateTime? Date { get; private set; }
    public string TariffNo { get; private set; }
    public string ItemDescription { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public short SourceType { get; private set; }
    public int SourceEntryNo { get; private set; }
    public decimal NetWeight { get; private set; }
    public decimal Amount { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal CostRegulation { get; private set; }
    public decimal IndirectCost { get; private set; }
    public decimal StatisticalValue { get; private set; }
    public string DocumentNo { get; private set; }
    public string ItemNo { get; private set; }
    public string Name { get; private set; }
    public decimal TotalWeight { get; private set; }
    public bool SupplementaryUnits { get; private set; }
    public string InternalRefNo { get; private set; }
    public string CountryRegionOfOriginCode { get; private set; }
    public string EntryExitPoint { get; private set; }
    public string Area { get; private set; }
    public string TransactionSpecification { get; private set; }

    public static OperationResult<IntrastatJnlLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IntrastatJnlLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IntrastatJnlLine()
        {
            TenantId = tenantId
        };
        return OperationResult<IntrastatJnlLine, DomainError>.Ok(entity);
    }
}
