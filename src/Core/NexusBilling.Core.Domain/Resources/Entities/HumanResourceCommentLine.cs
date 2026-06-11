using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class HumanResourceCommentLine : Entity
{
    private HumanResourceCommentLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short TableName { get; private set; }
    public string No { get; private set; }
    public int TableLineNo { get; private set; }
    public string AlternativeAddressCode { get; private set; }
    public int LineNo { get; private set; }
    public DateTime? Date { get; private set; }
    public string Code { get; private set; }
    public string Comment { get; private set; }

    public static OperationResult<HumanResourceCommentLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<HumanResourceCommentLine, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new HumanResourceCommentLine()
        {
            TenantId = tenantId
        };
        return OperationResult<HumanResourceCommentLine, DomainError>.Ok(entity);
    }
}
