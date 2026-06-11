using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceEmailQueueConfiguration : IEntityTypeConfiguration<ServiceEmailQueue>
{
    public void Configure(EntityTypeBuilder<ServiceEmailQueue> builder)
    {
        builder.ToTable("service_email_queue", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ToAddress).HasColumnName("to_address");
        builder.Property(x => x.CopyToAddress).HasColumnName("copy_to_address");
        builder.Property(x => x.SubjectLine).HasColumnName("subject_line");
        builder.Property(x => x.BodyLine).HasColumnName("body_line");
        builder.Property(x => x.AttachmentFilename).HasColumnName("attachment_filename");
        builder.Property(x => x.SendingDate).HasColumnName("sending_date");
        builder.Property(x => x.SendingTime).HasColumnName("sending_time");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
    }
}
