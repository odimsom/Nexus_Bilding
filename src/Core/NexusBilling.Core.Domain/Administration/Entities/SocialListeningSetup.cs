using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class SocialListeningSetup : Entity
{
    private SocialListeningSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string SolutionId { get; private set; }
    public bool ShowOnItems { get; private set; }
    public bool ShowOnCustomers { get; private set; }
    public bool ShowOnVendors { get; private set; }
    public bool AcceptLicenseAgreement { get; private set; }
    public string TermsOfUseUrl { get; private set; }
    public string SignupUrl { get; private set; }
    public string SocialListeningUrl { get; private set; }

    public static OperationResult<SocialListeningSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<SocialListeningSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new SocialListeningSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<SocialListeningSetup, DomainError>.Ok(entity);
    }
}
