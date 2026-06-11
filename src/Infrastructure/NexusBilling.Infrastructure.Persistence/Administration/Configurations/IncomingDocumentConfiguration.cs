using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class IncomingDocumentConfiguration : IEntityTypeConfiguration<IncomingDocument>
{
    public void Configure(EntityTypeBuilder<IncomingDocument> builder)
    {
        builder.ToTable("incoming_document", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CreatedDateTime).HasColumnName("created_date_time");
        builder.Property(x => x.CreatedByUserId).HasColumnName("created_by_user_id");
        builder.Property(x => x.Released).HasColumnName("released");
        builder.Property(x => x.ReleasedDateTime).HasColumnName("released_date_time");
        builder.Property(x => x.ReleasedByUserId).HasColumnName("released_by_user_id");
        builder.Property(x => x.LastDateTimeModified).HasColumnName("last_date_time_modified");
        builder.Property(x => x.LastModifiedByUserId).HasColumnName("last_modified_by_user_id");
        builder.Property(x => x.Posted).HasColumnName("posted");
        builder.Property(x => x.PostedDateTime).HasColumnName("posted_date_time");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.Url1).HasColumnName("url1");
        builder.Property(x => x.Url2).HasColumnName("url2");
        builder.Property(x => x.Url3).HasColumnName("url3");
        builder.Property(x => x.Url4).HasColumnName("url4");
        builder.Property(x => x.VendorName).HasColumnName("vendor_name");
        builder.Property(x => x.VendorVatRegistrationNo).HasColumnName("vendor_vat_registration_no");
        builder.Property(x => x.VendorIban).HasColumnName("vendor_iban");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.VendorBankBranchNo).HasColumnName("vendor_bank_branch_no");
        builder.Property(x => x.VendorBankAccountNo).HasColumnName("vendor_bank_account_no");
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.DataExchangeType).HasColumnName("data_exchange_type");
        builder.Property(x => x.OcrDataCorrected).HasColumnName("ocr_data_corrected");
        builder.Property(x => x.OcrStatus).HasColumnName("ocr_status");
        builder.Property(x => x.OcrServiceDocTemplateCode).HasColumnName("ocr_service_doc_template_code");
        builder.Property(x => x.OcrProcessFinished).HasColumnName("ocr_process_finished");
        builder.Property(x => x.CreatedDocErrorMsgType).HasColumnName("created_doc_error_msg_type");
        builder.Property(x => x.VendorId).HasColumnName("vendor_id");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.AmountExclVat).HasColumnName("amount_excl_vat").HasPrecision(18, 5);
        builder.Property(x => x.AmountInclVat).HasColumnName("amount_incl_vat").HasPrecision(18, 5);
        builder.Property(x => x.VatAmount).HasColumnName("vat_amount").HasPrecision(18, 5);
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.VendorInvoiceNo).HasColumnName("vendor_invoice_no");
        builder.Property(x => x.OrderNo).HasColumnName("order_no");
        builder.Property(x => x.VendorPhoneNo).HasColumnName("vendor_phone_no");
        builder.Property(x => x.RelatedRecordId).HasColumnName("related_record_id");
        builder.Property(x => x.JobQueueStatus).HasColumnName("job_queue_status");
        builder.Property(x => x.JobQueueEntryId).HasColumnName("job_queue_entry_id");
        builder.Property(x => x.Processed).HasColumnName("processed");
    }
}
