using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class PaymentJnlExportErrorTextConfiguration : IEntityTypeConfiguration<PaymentJnlExportErrorText>
{
    public void Configure(EntityTypeBuilder<PaymentJnlExportErrorText> builder)
    {
        builder.ToTable("payment_jnl_export_error_text", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.JournalLineNo).HasColumnName("journal_line_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.ErrorText).HasColumnName("error_text");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.AdditionalInformation).HasColumnName("additional_information");
        builder.Property(x => x.SupportUrl).HasColumnName("support_url");
    }
}
