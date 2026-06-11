using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class MaintenanceRegistration : Entity
{
    private MaintenanceRegistration() { }

    public TenantIdentifier TenantId { get; private set; }
    public string FaNo { get; private set; }
    public int LineNo { get; private set; }
    public DateTime? ServiceDate { get; private set; }
    public string MaintenanceVendorNo { get; private set; }
    public string Comment { get; private set; }
    public string ServiceAgentName { get; private set; }
    public string ServiceAgentPhoneNo { get; private set; }
    public string ServiceAgentMobilePhone { get; private set; }

    public static OperationResult<MaintenanceRegistration, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<MaintenanceRegistration, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new MaintenanceRegistration()
        {
            TenantId = tenantId
        };
        return OperationResult<MaintenanceRegistration, DomainError>.Ok(entity);
    }
}
