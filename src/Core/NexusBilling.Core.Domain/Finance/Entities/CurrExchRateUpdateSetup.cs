using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class CurrExchRateUpdateSetup : Entity
{
    private CurrExchRateUpdateSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public byte[]? WebServiceUrl { get; private set; }
    public bool Enabled { get; private set; }
    public string ServiceProvider { get; private set; }
    public string TermsOfService { get; private set; }
    public string DataExchDefCode { get; private set; }
    public bool LogWebRequests { get; private set; }

    public static OperationResult<CurrExchRateUpdateSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CurrExchRateUpdateSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CurrExchRateUpdateSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<CurrExchRateUpdateSetup, DomainError>.Ok(entity);
    }
}
