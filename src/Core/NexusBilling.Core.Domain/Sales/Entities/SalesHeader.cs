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
        BillToName = string.Empty;
        TenantId = TenantIdentifier.Create(Guid.Empty);
    }

    private SalesHeader(TenantIdentifier tenantId, string documentType, string no, string sellToCustomerNo, string billToName, DateTime postingDate)
    {
        Id = Guid.NewGuid();
        TenantId = tenantId;
        DocumentType = documentType;
        No = no;
        SellToCustomerNo = sellToCustomerNo;
        BillToName = billToName;
        PostingDate = postingDate;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TenantIdentifier TenantId { get; private set; }
    public string DocumentType { get; private set; }
    public string No { get; private set; }
    public string SellToCustomerNo { get; private set; }
    public string BillToName { get; private set; }
    public DateTime PostingDate { get; private set; }

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    public static OperationResult<SalesHeader, DomainError> Create(
        TenantIdentifier tenantId,
        string documentType,
        string no,
        string sellToCustomerNo,
        string billToName,
        DateTime postingDate)
    {
        if (tenantId.Value == Guid.Empty)
            return OperationResult<SalesHeader, DomainError>.Fail(DomainError.Validation("sales_header.tenant_required", "El TenantIdentifier es obligatorio."));

        if (string.IsNullOrWhiteSpace(no))
            return OperationResult<SalesHeader, DomainError>.Fail(DomainError.Validation("sales_header.no_required", "El número de documento es obligatorio."));

        if (string.IsNullOrWhiteSpace(sellToCustomerNo))
            return OperationResult<SalesHeader, DomainError>.Fail(DomainError.Validation("sales_header.customer_no_required", "El número de cliente es obligatorio."));

        return OperationResult<SalesHeader, DomainError>.Ok(new SalesHeader(tenantId, documentType.Trim(), no.Trim(), sellToCustomerNo.Trim(), billToName.Trim(), postingDate));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
    private void AddDomainEvent(object @event) => _domainEvents.Add(@event);
}
