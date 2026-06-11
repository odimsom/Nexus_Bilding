using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CashFlowWorksheetLine : Entity
{
    private CashFlowWorksheetLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public int LineNo { get; private set; }
    public string CashFlowForecastNo { get; private set; }
    public DateTime? CashFlowDate { get; private set; }
    public string DocumentNo { get; private set; }
    public string CashFlowAccountNo { get; private set; }
    public short SourceType { get; private set; }
    public string Description { get; private set; }
    public short DocumentType { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public DateTime? PmtDiscountDate { get; private set; }
    public DateTime? PmtDiscToleranceDate { get; private set; }
    public string PaymentTermsCode { get; private set; }
    public decimal PaymentDiscount { get; private set; }
    public int AssociatedEntryNo { get; private set; }
    public bool Overdue { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public decimal AmountLcy { get; private set; }
    public string SourceNo { get; private set; }
    public string GLBudgetName { get; private set; }
    public int DimensionSetId { get; private set; }

    public static OperationResult<CashFlowWorksheetLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CashFlowWorksheetLine, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CashFlowWorksheetLine()
        {
            TenantId = tenantId
        };
        return OperationResult<CashFlowWorksheetLine, DomainError>.Ok(entity);
    }
}
