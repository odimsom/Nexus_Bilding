using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AnalysisColumn : Entity
{
    private AnalysisColumn() { }

    public TenantIdentifier TenantId { get; private set; }
    public short AnalysisArea { get; private set; }
    public string AnalysisColumnTemplate { get; private set; }
    public int LineNo { get; private set; }
    public string ColumnNo { get; private set; }
    public string ColumnHeader { get; private set; }
    public short ColumnType { get; private set; }
    public short LedgerEntryType { get; private set; }
    public string Formula { get; private set; }
    public string ComparisonDateFormula { get; private set; }
    public bool ShowOppositeSign { get; private set; }
    public short Show { get; private set; }
    public short RoundingFactor { get; private set; }
    public string ComparisonPeriodFormula { get; private set; }
    public string AnalysisTypeCode { get; private set; }
    public string ItemLedgerEntryTypeFilter { get; private set; }
    public string ValueEntryTypeFilter { get; private set; }
    public short ValueType { get; private set; }
    public bool Invoiced { get; private set; }
    public int ComparisonPeriodFormulaLcid { get; private set; }

    public static OperationResult<AnalysisColumn, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AnalysisColumn, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AnalysisColumn()
        {
            TenantId = tenantId
        };
        return OperationResult<AnalysisColumn, DomainError>.Ok(entity);
    }
}
