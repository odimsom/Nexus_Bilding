using System;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GLAccount : Entity
{
    private readonly List<object> _domainEvents = [];

    private GLAccount()
    {
        No = string.Empty;
        Name = string.Empty;
        TenantId = TenantIdentifier.Create(Guid.Empty);
    }

    private GLAccount(TenantIdentifier tenantId, string no, string name, short accountType, short incomeBalance)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        No = no;
        Name = name;
        AccountType = accountType;
        IncomeBalance = incomeBalance;
        Blocked = false;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string Name { get; private set; }
    public short AccountType { get; private set; } // [Posting, Heading, Total, Begin-Total, End-Total]
    public short IncomeBalance { get; private set; } // [Income Statement, Balance Sheet]
    public bool Blocked { get; private set; }

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    public static OperationResult<GLAccount, DomainError> Create(
        TenantIdentifier tenantId,
        string no,
        string name,
        short accountType,
        short incomeBalance)
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<GLAccount, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        if (string.IsNullOrWhiteSpace(no))
            return OperationResult<GLAccount, DomainError>.Fail(DomainError.Validation("finance.no_required", "El número de cuenta es obligatorio."));

        if (string.IsNullOrWhiteSpace(name))
            return OperationResult<GLAccount, DomainError>.Fail(DomainError.Validation("finance.name_required", "El nombre de la cuenta es obligatorio."));

        return OperationResult<GLAccount, DomainError>.Ok(new GLAccount(tenantId, no.Trim(), name.Trim(), accountType, incomeBalance));
    }

    public void Block()
    {
        Blocked = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Unblock()
    {
        Blocked = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
    private void AddDomainEvent(object @event) => _domainEvents.Add(@event);
}
