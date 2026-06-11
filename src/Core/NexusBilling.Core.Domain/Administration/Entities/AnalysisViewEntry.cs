using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AnalysisViewEntry : Entity
{
    private AnalysisViewEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public string AnalysisViewCode { get; private set; }
    public string BusinessUnitCode { get; private set; }
    public string AccountNo { get; private set; }
    public string Dimension1ValueCode { get; private set; }
    public string Dimension2ValueCode { get; private set; }
    public string Dimension3ValueCode { get; private set; }
    public string Dimension4ValueCode { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public int EntryNo { get; private set; }
    public decimal Amount { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }
    public decimal AddCurrAmount { get; private set; }
    public decimal AddCurrDebitAmount { get; private set; }
    public decimal AddCurrCreditAmount { get; private set; }
    public short AccountSource { get; private set; }
    public string CashFlowForecastNo { get; private set; }

    public static OperationResult<AnalysisViewEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AnalysisViewEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AnalysisViewEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<AnalysisViewEntry, DomainError>.Ok(entity);
    }
}
