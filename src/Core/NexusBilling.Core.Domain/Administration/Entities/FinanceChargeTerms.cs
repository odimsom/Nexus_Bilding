using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class FinanceChargeTerms : Entity
{
    private FinanceChargeTerms() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public decimal InterestRate { get; private set; }
    public decimal MinimumAmountLcy { get; private set; }
    public decimal AdditionalFeeLcy { get; private set; }
    public string Description { get; private set; }
    public short InterestCalculationMethod { get; private set; }
    public int InterestPeriodDays { get; private set; }
    public string GracePeriod { get; private set; }
    public string DueDateCalculation { get; private set; }
    public short InterestCalculation { get; private set; }
    public bool PostInterest { get; private set; }
    public bool PostAdditionalFee { get; private set; }
    public string LineDescription { get; private set; }
    public bool AddLineFeeInInterest { get; private set; }

    public static OperationResult<FinanceChargeTerms, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<FinanceChargeTerms, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new FinanceChargeTerms()
        {
            TenantId = tenantId
        };
        return OperationResult<FinanceChargeTerms, DomainError>.Ok(entity);
    }
}
