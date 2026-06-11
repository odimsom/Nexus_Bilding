using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ExtendedTextLine : Entity
{
    private ExtendedTextLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short TableName { get; private set; }
    public string No { get; private set; }
    public string LanguageCode { get; private set; }
    public int TextNo { get; private set; }
    public int LineNo { get; private set; }
    public string Text { get; private set; }

    public static OperationResult<ExtendedTextLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ExtendedTextLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ExtendedTextLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ExtendedTextLine, DomainError>.Ok(entity);
    }
}
