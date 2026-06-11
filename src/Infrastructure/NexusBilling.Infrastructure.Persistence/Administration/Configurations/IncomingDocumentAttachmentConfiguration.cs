using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class IncomingDocumentAttachmentConfiguration : IEntityTypeConfiguration<IncomingDocumentAttachment>
{
    public void Configure(EntityTypeBuilder<IncomingDocumentAttachment> builder)
    {
        builder.ToTable("incoming_document_attachment", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IncomingDocumentEntryNo).HasColumnName("incoming_document_entry_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.CreatedDateTime).HasColumnName("created_date_time");
        builder.Property(x => x.CreatedByUserName).HasColumnName("created_by_user_name");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.FileExtension).HasColumnName("file_extension");
        builder.Property(x => x.Content).HasColumnName("content");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.Default).HasColumnName("default");
        builder.Property(x => x.UseForOcr).HasColumnName("use_for_ocr");
        builder.Property(x => x.ExternalDocumentReference).HasColumnName("external_document_reference");
        builder.Property(x => x.OcrServiceDocumentReference).HasColumnName("ocr_service_document_reference");
        builder.Property(x => x.GeneratedFromOcr).HasColumnName("generated_from_ocr");
        builder.Property(x => x.MainAttachment).HasColumnName("main_attachment");
    }
}
