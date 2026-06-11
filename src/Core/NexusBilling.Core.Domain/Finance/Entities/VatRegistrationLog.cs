using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class VatRegistrationLog : Entity
{
    private VatRegistrationLog() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string VatRegistrationNo { get; private set; }
    public short AccountType { get; private set; }
    public string AccountNo { get; private set; }
    public string CountryRegionCode { get; private set; }
    public string UserId { get; private set; }
    public short Status { get; private set; }
    public string VerifiedName { get; private set; }
    public string VerifiedAddress { get; private set; }
    public DateTime? VerifiedDate { get; private set; }

    public static OperationResult<VatRegistrationLog, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<VatRegistrationLog, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new VatRegistrationLog()
        {
            TenantId = tenantId
        };
        return OperationResult<VatRegistrationLog, DomainError>.Ok(entity);
    }
}
