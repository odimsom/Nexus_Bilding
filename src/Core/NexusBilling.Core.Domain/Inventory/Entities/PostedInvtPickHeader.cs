using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class PostedInvtPickHeader : Entity
{
    private PostedInvtPickHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public string No { get; private set; }
    public string LocationCode { get; private set; }
    public string AssignedUserId { get; private set; }
    public DateTime? AssignmentDate { get; private set; }
    public string AssignmentTime { get; private set; }
    public DateTime? RegisteringDate { get; private set; }
    public string NoSeries { get; private set; }
    public string InvtPickNo { get; private set; }
    public int NoPrinted { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public string SourceNo { get; private set; }
    public short SourceDocument { get; private set; }
    public int SourceType { get; private set; }
    public short SourceSubtype { get; private set; }
    public short DestinationType { get; private set; }
    public string DestinationNo { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public DateTime? ShipmentDate { get; private set; }
    public string ExternalDocumentNo2 { get; private set; }

    public static OperationResult<PostedInvtPickHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PostedInvtPickHeader, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new PostedInvtPickHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<PostedInvtPickHeader, DomainError>.Ok(entity);
    }
}
