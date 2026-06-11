using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AssistedSetup : Entity
{
    private AssistedSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public int PageId { get; private set; }
    public string Name { get; private set; }
    public int Order { get; private set; }
    public short Status { get; private set; }
    public bool Visible { get; private set; }
    public int Parent { get; private set; }
    public string VideoUrl { get; private set; }
    public Guid Icon { get; private set; }
    public short ItemType { get; private set; }
    public bool Featured { get; private set; }
    public string HelpUrl { get; private set; }
    public int AssistedSetupPageId { get; private set; }
    public int TourId { get; private set; }
    public bool VideoStatus { get; private set; }
    public bool HelpStatus { get; private set; }
    public bool TourStatus { get; private set; }

    public static OperationResult<AssistedSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AssistedSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AssistedSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<AssistedSetup, DomainError>.Ok(entity);
    }
}
