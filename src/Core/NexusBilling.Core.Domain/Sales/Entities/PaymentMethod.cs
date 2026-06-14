using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class PaymentMethod : Entity
{
    private PaymentMethod() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public short BalAccountType { get; private set; }
    public string BalAccountNo { get; private set; } = string.Empty;
    public bool DirectDebit { get; private set; }
    public string DirectDebitPmtTermsCode { get; private set; } = string.Empty;
    public string PmtExportLineDefinition { get; private set; } = string.Empty;
    public string BankDataConversionPmtType { get; private set; } = string.Empty;

    public static OperationResult<PaymentMethod, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PaymentMethod, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PaymentMethod()
        {
            TenantId = tenantId
        };
        return OperationResult<PaymentMethod, DomainError>.Ok(entity);
    }
}
