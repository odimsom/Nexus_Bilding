using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Security.Entities;

public class PageDataPersonalization : Entity
{
    private PageDataPersonalization() { }

    public TenantIdentifier TenantId { get; private set; }
    public Guid UserSid { get; private set; }
    public short ObjectType { get; private set; }
    public int ObjectId { get; private set; }
    public DateTime? Date { get; private set; }
    public string Time { get; private set; }
    public string PersonalizationId { get; private set; }
    public string Valuename { get; private set; }
    public byte[]? Value { get; private set; }

    public static OperationResult<PageDataPersonalization, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PageDataPersonalization, DomainError>.Fail(DomainError.Validation("security.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PageDataPersonalization()
        {
            TenantId = tenantId
        };
        return OperationResult<PageDataPersonalization, DomainError>.Ok(entity);
    }
}
