using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ServPriceGroupSetup : Entity
{
    private ServPriceGroupSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string ServicePriceGroupCode { get; private set; }
    public string FaultAreaCode { get; private set; }
    public string CustPriceGroupCode { get; private set; }
    public string CurrencyCode { get; private set; }
    public DateTime? StartingDate { get; private set; }
    public string ServPriceAdjmtGrCode { get; private set; }
    public bool IncludeDiscounts { get; private set; }
    public short AdjustmentType { get; private set; }
    public decimal Amount { get; private set; }
    public bool IncludeVat { get; private set; }

    public static OperationResult<ServPriceGroupSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ServPriceGroupSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ServPriceGroupSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<ServPriceGroupSetup, DomainError>.Ok(entity);
    }
}
