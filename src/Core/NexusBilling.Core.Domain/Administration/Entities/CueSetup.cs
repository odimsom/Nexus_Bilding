using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class CueSetup : Entity
{
    private CueSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserName { get; private set; }
    public int TableId { get; private set; }
    public int FieldNo { get; private set; }
    public short LowRangeStyle { get; private set; }
    public decimal Threshold1 { get; private set; }
    public short MiddleRangeStyle { get; private set; }
    public decimal Threshold2 { get; private set; }
    public short HighRangeStyle { get; private set; }
    public bool Personalized { get; private set; }

    public static OperationResult<CueSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CueSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CueSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<CueSetup, DomainError>.Ok(entity);
    }
}
