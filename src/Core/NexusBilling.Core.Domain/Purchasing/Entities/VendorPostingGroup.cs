using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class VendorPostingGroup : Entity
{
    private VendorPostingGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string PayablesAccount { get; private set; }
    public string ServiceChargeAcc { get; private set; }
    public string PaymentDiscDebitAcc { get; private set; }
    public string InvoiceRoundingAccount { get; private set; }
    public string DebitCurrApplnRndgAcc { get; private set; }
    public string CreditCurrApplnRndgAcc { get; private set; }
    public string DebitRoundingAccount { get; private set; }
    public string CreditRoundingAccount { get; private set; }
    public string PaymentDiscCreditAcc { get; private set; }
    public string PaymentToleranceDebitAcc { get; private set; }
    public string PaymentToleranceCreditAcc { get; private set; }

    public static OperationResult<VendorPostingGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<VendorPostingGroup, DomainError>.Fail(DomainError.Validation("purchasing.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new VendorPostingGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<VendorPostingGroup, DomainError>.Ok(entity);
    }
}
