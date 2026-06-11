using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class PostedDocsWithNoIncBuf : Entity
{
    private PostedDocsWithNoIncBuf() { }

    public TenantIdentifier TenantId { get; private set; }
    public int LineNo { get; private set; }
    public string DocumentNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public string FirstPostingDescription { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public decimal DebitAmount { get; private set; }
    public decimal CreditAmount { get; private set; }

    public static OperationResult<PostedDocsWithNoIncBuf, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PostedDocsWithNoIncBuf, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PostedDocsWithNoIncBuf()
        {
            TenantId = tenantId
        };
        return OperationResult<PostedDocsWithNoIncBuf, DomainError>.Ok(entity);
    }
}
