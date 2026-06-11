using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class IncomingDocumentsSetup : Entity
{
    private IncomingDocumentsSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string PrimaryKey { get; private set; }
    public string GeneralJournalTemplateName { get; private set; }
    public string GeneralJournalBatchName { get; private set; }
    public bool RequireApprovalToCreate { get; private set; }
    public bool RequireApprovalToPost { get; private set; }

    public static OperationResult<IncomingDocumentsSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IncomingDocumentsSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IncomingDocumentsSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<IncomingDocumentsSetup, DomainError>.Ok(entity);
    }
}
