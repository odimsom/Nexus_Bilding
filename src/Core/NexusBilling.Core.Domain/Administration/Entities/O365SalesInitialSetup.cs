using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class O365SalesInitialSetup : Entity
{
    private O365SalesInitialSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string PaymentRegTemplateName { get; private set; }
    public string PaymentRegBatchName { get; private set; }
    public bool IsInitialized { get; private set; }
    public string DefaultCustomerTemplate { get; private set; }
    public string DefaultItemTemplate { get; private set; }
    public string DefaultPaymentTermsCode { get; private set; }
    public string DefaultPaymentMethodCode { get; private set; }
    public string SalesInvoiceNoSeries { get; private set; }
    public string PostedSalesInvNoSeries { get; private set; }

    public static OperationResult<O365SalesInitialSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<O365SalesInitialSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new O365SalesInitialSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<O365SalesInitialSetup, DomainError>.Ok(entity);
    }
}
