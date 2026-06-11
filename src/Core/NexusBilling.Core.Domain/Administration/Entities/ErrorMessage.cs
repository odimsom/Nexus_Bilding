using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ErrorMessage : Entity
{
    private ErrorMessage() { }

    public TenantIdentifier TenantId { get; private set; }
    public int IdNav { get; private set; }
    public string RecordId { get; private set; }
    public int FieldNumber { get; private set; }
    public short MessageType { get; private set; }
    public string Description { get; private set; }
    public string AdditionalInformation { get; private set; }
    public string SupportUrl { get; private set; }
    public int TableNumber { get; private set; }
    public string ContextRecordId { get; private set; }

    public static OperationResult<ErrorMessage, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ErrorMessage, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ErrorMessage()
        {
            TenantId = tenantId
        };
        return OperationResult<ErrorMessage, DomainError>.Ok(entity);
    }
}
