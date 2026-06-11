using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ColumnLayout : Entity
{
    private ColumnLayout() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ColumnLayoutName { get; private set; }
    public int LineNo { get; private set; }
    public string ColumnNo { get; private set; }
    public string ColumnHeader { get; private set; }
    public short ColumnType { get; private set; }
    public short LedgerEntryType { get; private set; }
    public short AmountType { get; private set; }
    public string Formula { get; private set; }
    public string ComparisonDateFormula { get; private set; }
    public bool ShowOppositeSign { get; private set; }
    public short Show { get; private set; }
    public short RoundingFactor { get; private set; }
    public short ShowIndentedLines { get; private set; }
    public string ComparisonPeriodFormula { get; private set; }
    public string BusinessUnitTotaling { get; private set; }
    public string Dimension1Totaling { get; private set; }
    public string Dimension2Totaling { get; private set; }
    public string Dimension3Totaling { get; private set; }
    public string Dimension4Totaling { get; private set; }
    public string CostCenterTotaling { get; private set; }
    public string CostObjectTotaling { get; private set; }
    public int ComparisonPeriodFormulaLcid { get; private set; }

    public static OperationResult<ColumnLayout, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ColumnLayout, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ColumnLayout()
        {
            TenantId = tenantId
        };
        return OperationResult<ColumnLayout, DomainError>.Ok(entity);
    }
}
