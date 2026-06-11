using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class TenantWebServiceOdata : Entity
{
    private TenantWebServiceOdata() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Tenantwebserviceid { get; private set; }
    public byte[]? Odataselectclause { get; private set; }
    public byte[]? Odatafilterclause { get; private set; }

    public static OperationResult<TenantWebServiceOdata, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TenantWebServiceOdata, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new TenantWebServiceOdata()
        {
            TenantId = tenantId
        };
        return OperationResult<TenantWebServiceOdata, DomainError>.Ok(entity);
    }
}
