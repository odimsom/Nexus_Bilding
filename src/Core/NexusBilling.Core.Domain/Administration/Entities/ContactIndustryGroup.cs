using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ContactIndustryGroup : Entity
{
    private ContactIndustryGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ContactNo { get; private set; }
    public string IndustryGroupCode { get; private set; }

    public static OperationResult<ContactIndustryGroup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ContactIndustryGroup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ContactIndustryGroup()
        {
            TenantId = tenantId
        };
        return OperationResult<ContactIndustryGroup, DomainError>.Ok(entity);
    }
}
