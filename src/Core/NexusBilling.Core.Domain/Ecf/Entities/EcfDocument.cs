using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Ecf.Enums;

namespace NexusBilling.Core.Domain.Ecf.Entities;

public class EcfDocument : Entity
{
    private EcfDocument()
    {
        Ncf = string.Empty;
        XmlContent = string.Empty;
        TrackId = string.Empty;
        SecurityCode = string.Empty;
        TenantId = null!;
    }

    private EcfDocument(
        TenantIdentifier tenantId,
        Guid sourceDocumentId,
        string ncf,
        string xmlContent)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        SourceDocumentId = sourceDocumentId;
        Ncf = ncf;
        XmlContent = xmlContent;
        Status = EcfDocumentStatus.Draft;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        TrackId = string.Empty;
        SecurityCode = string.Empty;
    }

    public TenantIdentifier TenantId { get; private set; }
    public Guid SourceDocumentId { get; private set; } // ID de la Factura, Nota de Crédito, etc.
    public string Ncf { get; private set; }
    public string XmlContent { get; private set; }
    public EcfDocumentStatus Status { get; private set; }
    public string TrackId { get; private set; }
    public string SecurityCode { get; private set; }
    public string? RejectionReason { get; private set; }

    public static OperationResult<EcfDocument, DomainError> Create(
        TenantIdentifier tenantId,
        Guid sourceDocumentId,
        string ncf,
        string xmlContent)
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<EcfDocument, DomainError>.Fail(DomainError.Validation("ecf_doc.tenant_required", "El TenantIdentifier es obligatorio."));

        return OperationResult<EcfDocument, DomainError>.Ok(new EcfDocument(tenantId, sourceDocumentId, ncf, xmlContent));
    }

    public void MarkAsSigned(string signedXml)
    {
        XmlContent = signedXml;
        Status = EcfDocumentStatus.Signed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsSent(string trackId)
    {
        TrackId = trackId;
        Status = EcfDocumentStatus.Sent;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsApproved(string securityCode)
    {
        SecurityCode = securityCode;
        Status = EcfDocumentStatus.Approved;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsRejected(string reason)
    {
        RejectionReason = reason;
        Status = EcfDocumentStatus.Rejected;
        UpdatedAt = DateTime.UtcNow;
    }
}
