using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class StandardCustomerSalesCode : Entity
{
    private StandardCustomerSalesCode() { }

    public TenantIdentifier TenantId { get; private set; }
    public string CustomerNo { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public DateTime? ValidFromDate { get; private set; }
    public DateTime? ValidToDate { get; private set; }
    public string PaymentMethodCode { get; private set; }
    public string PaymentTermsCode { get; private set; }
    public string DirectDebitMandateId { get; private set; }
    public bool Blocked { get; private set; }

    public static OperationResult<StandardCustomerSalesCode, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<StandardCustomerSalesCode, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new StandardCustomerSalesCode()
        {
            TenantId = tenantId
        };
        return OperationResult<StandardCustomerSalesCode, DomainError>.Ok(entity);
    }
}
