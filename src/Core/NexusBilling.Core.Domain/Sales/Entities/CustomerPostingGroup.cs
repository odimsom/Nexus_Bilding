using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class CustomerPostingGroup : Entity
{
    private CustomerPostingGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string ReceivablesAccount { get; private set; }
    public string ServiceChargeAcc { get; private set; }
    public string PaymentDiscDebitAcc { get; private set; }
    public string InvoiceRoundingAccount { get; private set; }
    public string AdditionalFeeAccount { get; private set; }
    public string InterestAccount { get; private set; }
    public string DebitCurrApplnRndgAcc { get; private set; }
    public string CreditCurrApplnRndgAcc { get; private set; }
    public string DebitRoundingAccount { get; private set; }
    public string CreditRoundingAccount { get; private set; }
    public string PaymentDiscCreditAcc { get; private set; }
    public string PaymentToleranceDebitAcc { get; private set; }
    public string PaymentToleranceCreditAcc { get; private set; }
    public string AddFeePerLineAccount { get; private set; }

    public static OperationResult<CustomerPostingGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CustomerPostingGroup, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CustomerPostingGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<CustomerPostingGroup, DomainError>.Ok(entity);
    }
}
