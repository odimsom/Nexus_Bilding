using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class BusinessChartUserSetup : Entity
{
    private BusinessChartUserSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string UserId { get; private set; }
    public short ObjectType { get; private set; }
    public int ObjectId { get; private set; }
    public short PeriodLength { get; private set; }

    public static OperationResult<BusinessChartUserSetup, DomainError> Create(
        TenantIdentifier tenantId,
        string userId,
        short objectType,
        int objectId,
        short periodLength)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<BusinessChartUserSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(userId))
            return OperationResult<BusinessChartUserSetup, DomainError>.Fail(DomainError.Validation("finance.user_id_required", "El campo user_id es obligatorio."));

        var entity = new BusinessChartUserSetup()
        {
            TenantId = tenantId,
            UserId = userId.Trim(),
            ObjectType = objectType,
            ObjectId = objectId,
            PeriodLength = periodLength,
        };

        return OperationResult<BusinessChartUserSetup, DomainError>.Ok(entity);
    }
}
