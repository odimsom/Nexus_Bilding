using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class IncomingDocument : Entity
{
    private IncomingDocument() { }

    public TenantIdentifier TenantId { get; private set; }
    public int EntryNo { get; private set; }
    public string Description { get; private set; }
    public DateTime? CreatedDateTime { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public bool Released { get; private set; }
    public DateTime? ReleasedDateTime { get; private set; }
    public Guid ReleasedByUserId { get; private set; }
    public DateTime? LastDateTimeModified { get; private set; }
    public Guid LastModifiedByUserId { get; private set; }
    public bool Posted { get; private set; }
    public DateTime? PostedDateTime { get; private set; }
    public short DocumentType { get; private set; }
    public string DocumentNo { get; private set; }
    public DateTime? PostingDate { get; private set; }
    public short Status { get; private set; }
    public string Url1 { get; private set; }
    public string Url2 { get; private set; }
    public string Url3 { get; private set; }
    public string Url4 { get; private set; }
    public string VendorName { get; private set; }
    public string VendorVatRegistrationNo { get; private set; }
    public string VendorIban { get; private set; }
    public DateTime? DocumentDate { get; private set; }
    public string VendorBankBranchNo { get; private set; }
    public string VendorBankAccountNo { get; private set; }
    public string VendorNo { get; private set; }
    public string DataExchangeType { get; private set; }
    public bool OcrDataCorrected { get; private set; }
    public short OcrStatus { get; private set; }
    public string OcrServiceDocTemplateCode { get; private set; }
    public bool OcrProcessFinished { get; private set; }
    public short CreatedDocErrorMsgType { get; private set; }
    public Guid VendorId { get; private set; }
    public string CurrencyCode { get; private set; }
    public decimal AmountExclVat { get; private set; }
    public decimal AmountInclVat { get; private set; }
    public decimal VatAmount { get; private set; }
    public DateTime? DueDate { get; private set; }
    public string VendorInvoiceNo { get; private set; }
    public string OrderNo { get; private set; }
    public string VendorPhoneNo { get; private set; }
    public string RelatedRecordId { get; private set; }
    public short JobQueueStatus { get; private set; }
    public Guid JobQueueEntryId { get; private set; }
    public bool Processed { get; private set; }

    public static OperationResult<IncomingDocument, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<IncomingDocument, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new IncomingDocument()
        {
            TenantId = tenantId
        };
        return OperationResult<IncomingDocument, DomainError>.Ok(entity);
    }
}
