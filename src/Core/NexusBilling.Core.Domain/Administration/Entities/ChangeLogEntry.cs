using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ChangeLogEntry : Entity
{
    private ChangeLogEntry() { }

    public TenantIdentifier TenantId { get; private set; }
    public long EntryNo { get; private set; }
    public DateTime? DateAndTime { get; private set; }
    public string Time { get; private set; }
    public string UserId { get; private set; }
    public int TableNo { get; private set; }
    public int FieldNo { get; private set; }
    public short TypeOfChange { get; private set; }
    public string OldValue { get; private set; }
    public string NewValue { get; private set; }
    public string PrimaryKey { get; private set; }
    public int PrimaryKeyField1No { get; private set; }
    public string PrimaryKeyField1Value { get; private set; }
    public int PrimaryKeyField2No { get; private set; }
    public string PrimaryKeyField2Value { get; private set; }
    public int PrimaryKeyField3No { get; private set; }
    public string PrimaryKeyField3Value { get; private set; }
    public string RecordId { get; private set; }

    public static OperationResult<ChangeLogEntry, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ChangeLogEntry, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ChangeLogEntry()
        {
            TenantId = tenantId
        };
        return OperationResult<ChangeLogEntry, DomainError>.Ok(entity);
    }
}
