using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalespersonPurchaser : Entity
{
    private SalespersonPurchaser() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Commission { get; private set; }
    public Guid Image { get; private set; }
    public string GlobalDimension1Code { get; private set; }
    public string GlobalDimension2Code { get; private set; }
    public string EMail { get; set; }
    public string PhoneNo { get; set; }
    public string JobTitle { get; set; }
    public string SearchEMail { get; private set; }
    public string EMail2 { get; private set; }

    public static OperationResult<SalespersonPurchaser, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SalespersonPurchaser, DomainError>.Fail(DomainError.Validation("sales.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SalespersonPurchaser()
        {
            TenantId = tenantId
        };
        return OperationResult<SalespersonPurchaser, DomainError>.Ok(entity);
    }
}
