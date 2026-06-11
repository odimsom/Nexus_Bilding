using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class AccountingPeriod : Entity
{
    private AccountingPeriod() { }

    public TenantIdentifier TenantId { get; private set; }
    public DateTime StartingDate { get; private set; }
    public string Name { get; private set; }
    public bool NewFiscalYear { get; private set; }
    public bool Closed { get; private set; }
    public bool DateLocked { get; private set; }
    public short AverageCostCalcType { get; private set; }
    public short AverageCostPeriod { get; private set; }

    public static OperationResult<AccountingPeriod, DomainError> Create(
        TenantIdentifier tenantId,
        DateTime startingDate,
        string name,
        bool newFiscalYear,
        bool closed,
        bool dateLocked,
        short averageCostCalcType,
        short averageCostPeriod)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AccountingPeriod, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(name))
            return OperationResult<AccountingPeriod, DomainError>.Fail(DomainError.Validation("finance.name_required", "El campo name es obligatorio."));

        var entity = new AccountingPeriod()
        {
            TenantId = tenantId,
            StartingDate = startingDate,
            Name = name.Trim(),
            NewFiscalYear = newFiscalYear,
            Closed = closed,
            DateLocked = dateLocked,
            AverageCostCalcType = averageCostCalcType,
            AverageCostPeriod = averageCostPeriod,
        };

        return OperationResult<AccountingPeriod, DomainError>.Ok(entity);
    }
}
