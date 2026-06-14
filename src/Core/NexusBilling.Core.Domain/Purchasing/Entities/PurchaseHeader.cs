using System;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class PurchaseHeader : Entity
{
    private readonly List<object> _domainEvents = [];

    private PurchaseHeader()
    {
        DocumentType = string.Empty;
        No = string.Empty;
        BuyFromVendorNo = string.Empty;
        PayToName = string.Empty;
        TenantId = null!;
    }

    private PurchaseHeader(TenantIdentifier tenantId, string documentType, string no, string buyFromVendorNo, string payToName, DateTime postingDate)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        DocumentType = documentType;
        No = no;
        BuyFromVendorNo = buyFromVendorNo;
        PayToName = payToName;
        PostingDate = postingDate;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string DocumentType { get; private set; } = string.Empty;
    public string No { get; private set; } = string.Empty;
    public string BuyFromVendorNo { get; private set; } = string.Empty;
    public string PayToName { get; private set; } = string.Empty;
    public DateTime PostingDate { get; set; }
    public DateTime? DueDate { get; set; }
    public string Status { get; set; } = "Open";
    public decimal Amount { get; set; }
    public decimal AmountIncludingVat { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string PaymentTermsCode { get; set; } = string.Empty;
    public string ExternalDocumentNo { get; set; } = string.Empty;

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    public static OperationResult<PurchaseHeader, DomainError> Create(
        TenantIdentifier tenantId,
        string documentType,
        string no,
        string buyFromVendorNo,
        string payToName,
        DateTime postingDate)
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<PurchaseHeader, DomainError>.Fail(DomainError.Validation("purchase_header.tenant_required", "El TenantIdentifier es obligatorio."));

        if (string.IsNullOrWhiteSpace(no))
            return OperationResult<PurchaseHeader, DomainError>.Fail(DomainError.Validation("purchase_header.no_required", "El número de documento es obligatorio."));

        if (string.IsNullOrWhiteSpace(buyFromVendorNo))
            return OperationResult<PurchaseHeader, DomainError>.Fail(DomainError.Validation("purchase_header.vendor_no_required", "El número de proveedor es obligatorio."));

        return OperationResult<PurchaseHeader, DomainError>.Ok(new PurchaseHeader(tenantId, documentType.Trim(), no.Trim(), buyFromVendorNo.Trim(), payToName.Trim(), postingDate));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
    private void AddDomainEvent(object @event) => _domainEvents.Add(@event);
}
