using System;
using System.Collections.Generic;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Sales.Entities;

public class SalesHeader : Entity
{
    private readonly List<object> _domainEvents = [];

    private SalesHeader()
    {
        DocumentType = string.Empty;
        No = string.Empty;
        SellToCustomerNo = string.Empty;
        SellToCustomerName = string.Empty;
        BillToName = string.Empty;
        CurrencyCode = string.Empty;
        PaymentTermsCode = string.Empty;
        PaymentMethodCode = string.Empty;
        SalespersonCode = string.Empty;
        ExternalDocumentNo = string.Empty;
        Status = "Open";
        TenantId = null!;
    }

    private SalesHeader(TenantIdentifier tenantId, string documentType, string no, string sellToCustomerNo, string sellToCustomerName, string billToName, DateTime postingDate)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        DocumentType = documentType;
        No = no;
        SellToCustomerNo = sellToCustomerNo;
        SellToCustomerName = sellToCustomerName;
        BillToName = billToName;
        PostingDate = postingDate;
        CurrencyCode = string.Empty;
        PaymentTermsCode = string.Empty;
        PaymentMethodCode = string.Empty;
        SalespersonCode = string.Empty;
        ExternalDocumentNo = string.Empty;
        Status = "Open";
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string DocumentType { get; private set; }
    public string No { get; private set; }
    public string SellToCustomerNo { get; private set; }
    public string SellToCustomerName { get; set; } = string.Empty;
    public string BillToName { get; private set; }
    public DateTime PostingDate { get; private set; }
    public DateTime? DueDate { get; set; }
    public decimal Amount { get; set; }
    public decimal AmountIncludingVat { get; set; }
    public string CurrencyCode { get; set; }
    public string PaymentTermsCode { get; set; }
    public string PaymentMethodCode { get; set; }
    public string SalespersonCode { get; set; }
    public string ExternalDocumentNo { get; set; }
    public string Status { get; set; }

    // Cotización fields
    public DateTime? ValidUntilDate { get; set; }
    public string? QuotedBy { get; set; }
    public string? Observations { get; set; }

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    public static OperationResult<SalesHeader, DomainError> Create(
        TenantIdentifier tenantId,
        string documentType,
        string no,
        string sellToCustomerNo,
        string sellToCustomerName,
        string billToName,
        DateTime postingDate)
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<SalesHeader, DomainError>.Fail(DomainError.Validation("sales_header.tenant_required", "El TenantIdentifier es obligatorio."));

        if (string.IsNullOrWhiteSpace(no))
            return OperationResult<SalesHeader, DomainError>.Fail(DomainError.Validation("sales_header.no_required", "El número de documento es obligatorio."));

        if (string.IsNullOrWhiteSpace(sellToCustomerNo))
            return OperationResult<SalesHeader, DomainError>.Fail(DomainError.Validation("sales_header.customer_no_required", "El número de cliente es obligatorio."));

        return OperationResult<SalesHeader, DomainError>.Ok(new SalesHeader(tenantId, documentType.Trim(), no.Trim(), sellToCustomerNo.Trim(), sellToCustomerName.Trim(), billToName.Trim(), postingDate));
    }

    public void Release() { Status = "Released"; UpdatedAt = DateTime.UtcNow; }
    public void Close()   { Status = "Closed";   UpdatedAt = DateTime.UtcNow; }

    public void ClearDomainEvents() => _domainEvents.Clear();
    private void AddDomainEvent(object @event) => _domainEvents.Add(@event);
}
