using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class TrialBalanceSetup : Entity
{
    private TrialBalanceSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string AccountScheduleName { get; private set; }
    public string ColumnLayoutName { get; private set; }

    public static OperationResult<TrialBalanceSetup, DomainError> Create(
        TenantIdentifier tenantId,
        string primaryKey,
        string accountScheduleName,
        string columnLayoutName)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<TrialBalanceSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(primaryKey))
            return OperationResult<TrialBalanceSetup, DomainError>.Fail(DomainError.Validation("finance.primary_key_required", "El campo primary_key es obligatorio."));
        if (string.IsNullOrWhiteSpace(accountScheduleName))
            return OperationResult<TrialBalanceSetup, DomainError>.Fail(DomainError.Validation("finance.account_schedule_name_required", "El campo account_schedule_name es obligatorio."));
        if (string.IsNullOrWhiteSpace(columnLayoutName))
            return OperationResult<TrialBalanceSetup, DomainError>.Fail(DomainError.Validation("finance.column_layout_name_required", "El campo column_layout_name es obligatorio."));

        var entity = new TrialBalanceSetup()
        {
            TenantId = tenantId,
            PrimaryKey = primaryKey.Trim(),
            AccountScheduleName = accountScheduleName.Trim(),
            ColumnLayoutName = columnLayoutName.Trim(),
        };

        return OperationResult<TrialBalanceSetup, DomainError>.Ok(entity);
    }
}
