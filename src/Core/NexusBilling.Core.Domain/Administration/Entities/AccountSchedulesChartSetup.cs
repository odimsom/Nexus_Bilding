using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class AccountSchedulesChartSetup : Entity
{
    private AccountSchedulesChartSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string AccountScheduleName { get; private set; }
    public string ColumnLayoutName { get; private set; }
    public short BaseXAxisOn { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public short PeriodLength { get; private set; }
    public int NoOfPeriods { get; private set; }
    public bool LastViewed { get; private set; }
    public bool LookAhead { get; private set; }

    public static OperationResult<AccountSchedulesChartSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<AccountSchedulesChartSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new AccountSchedulesChartSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<AccountSchedulesChartSetup, DomainError>.Ok(entity);
    }
}
