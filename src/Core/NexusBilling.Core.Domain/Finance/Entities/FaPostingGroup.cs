using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class FaPostingGroup : Entity
{
    private FaPostingGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string AcquisitionCostAccount { get; private set; }
    public string AccumDepreciationAccount { get; private set; }
    public string WriteDownAccount { get; private set; }
    public string AppreciationAccount { get; private set; }
    public string Custom1Account { get; private set; }
    public string Custom2Account { get; private set; }
    public string AcqCostAccOnDisposal { get; private set; }
    public string AccumDeprAccOnDisposal { get; private set; }
    public string WriteDownAccOnDisposal { get; private set; }
    public string AppreciationAccOnDisposal { get; private set; }
    public string Custom1AccountOnDisposal { get; private set; }
    public string Custom2AccountOnDisposal { get; private set; }
    public string GainsAccOnDisposal { get; private set; }
    public string LossesAccOnDisposal { get; private set; }
    public string BookValAccOnDispGain { get; private set; }
    public string SalesAccOnDispGain { get; private set; }
    public string WriteDownBalAccOnDisp { get; private set; }
    public string ApprecBalAccOnDisp { get; private set; }
    public string Custom1BalAccOnDisposal { get; private set; }
    public string Custom2BalAccOnDisposal { get; private set; }
    public string MaintenanceExpenseAccount { get; private set; }
    public string MaintenanceBalAcc { get; private set; }
    public string AcquisitionCostBalAcc { get; private set; }
    public string DepreciationExpenseAcc { get; private set; }
    public string WriteDownExpenseAcc { get; private set; }
    public string AppreciationBalAccount { get; private set; }
    public string Custom1ExpenseAcc { get; private set; }
    public string Custom2ExpenseAcc { get; private set; }
    public string SalesBalAcc { get; private set; }
    public string SalesAccOnDispLoss { get; private set; }
    public string BookValAccOnDispLoss { get; private set; }

    public static OperationResult<FaPostingGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FaPostingGroup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FaPostingGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<FaPostingGroup, DomainError>.Ok(entity);
    }
}
