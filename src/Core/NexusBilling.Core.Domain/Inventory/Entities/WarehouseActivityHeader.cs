using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Inventory.Entities;

public class WarehouseActivityHeader : Entity
{
    private WarehouseActivityHeader() { }

    public TenantIdentifier TenantId { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string LocationCode { get; private set; }
    public string AssignedUserId { get; private set; }
    public DateTime? AssignmentDate { get; private set; }
    public string AssignmentTime { get; private set; }
    public short SortingMethod { get; private set; }
    public string NoSeries { get; private set; }
    public int NoPrinted { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public string RegisteringNo { get; private set; }
    public string LastRegisteringNo { get; private set; }
    public string RegisteringNoSeries { get; private set; }
    public DateTime? DateOfLastPrinting { get; private set; }
    public string TimeOfLastPrinting { get; private set; }
    public bool BreakbulkFilter { get; private set; }
    public string SourceNo { get; private set; }
    public short SourceDocument { get; private set; }
    public int SourceType { get; private set; }
    public short SourceSubtype { get; private set; }
    public short DestinationType { get; private set; }
    public string DestinationNo { get; private set; }
    public string ExternalDocumentNo { get; private set; }
    public DateTime? ExpectedReceiptDate { get; private set; }
    public DateTime? ShipmentDate { get; private set; }
    public string ExternalDocumentNo2 { get; private set; }

    public static OperationResult<WarehouseActivityHeader, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WarehouseActivityHeader, DomainError>.Fail(DomainError.Validation("inventory.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WarehouseActivityHeader()
        {
            TenantId = tenantId
        };
        return OperationResult<WarehouseActivityHeader, DomainError>.Ok(entity);
    }
}
