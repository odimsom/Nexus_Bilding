using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class ExchRateAdjmtReg : Entity
{
    private ExchRateAdjmtReg() { }

    public TenantIdentifier TenantId { get; private set; }
    public int No { get; private set; }
    public DateTime? CreationDate { get; private set; }
    public short AccountType { get; private set; }
    public string PostingGroup { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal CurrencyFactor { get; private set; }
    public decimal AdjustedBase { get; private set; }
    public decimal AdjustedBaseLcy { get; private set; }
    public decimal AdjustedAmtLcy { get; private set; }
    public decimal AdjustedBaseAddCurr { get; private set; }
    public decimal AdjustedAmtAddCurr { get; private set; }

    public static OperationResult<ExchRateAdjmtReg, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ExchRateAdjmtReg, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ExchRateAdjmtReg()
        {
            TenantId = tenantId
        };
        return OperationResult<ExchRateAdjmtReg, DomainError>.Ok(entity);
    }
}
