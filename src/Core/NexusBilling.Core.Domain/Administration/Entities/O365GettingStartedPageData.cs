using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class O365GettingStartedPageData : Entity
{
    private O365GettingStartedPageData() { }

    public TenantIdentifier TenantId { get; private set; }
    public int No { get; private set; }
    public string DisplayTarget { get; private set; }
    public int WizardId { get; private set; }
    public short Type { get; private set; }
    public Guid Image { get; private set; }
    public byte[]? BodyText { get; private set; }

    public static OperationResult<O365GettingStartedPageData, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<O365GettingStartedPageData, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new O365GettingStartedPageData()
        {
            TenantId = tenantId
        };
        return OperationResult<O365GettingStartedPageData, DomainError>.Ok(entity);
    }
}
