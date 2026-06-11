using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class PostCode : Entity
{
    private PostCode() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string City { get; private set; }
    public string SearchCity { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string County { get; private set; }

    public static OperationResult<PostCode, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PostCode, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PostCode()
        {
            TenantId = tenantId
        };
        return OperationResult<PostCode, DomainError>.Ok(entity);
    }
}
