using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class PaymentServiceSetup : Entity
{
    private PaymentServiceSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Enabled { get; private set; }
    public bool AlwaysIncludeOnDocuments { get; private set; }
    public string SetupRecordId { get; private set; }
    public int SetupPageId { get; private set; }
    public string TermsOfService { get; private set; }
    public bool Available { get; private set; }
    public int ManagementCodeunitId { get; private set; }

    public static OperationResult<PaymentServiceSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PaymentServiceSetup, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PaymentServiceSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<PaymentServiceSetup, DomainError>.Ok(entity);
    }
}
