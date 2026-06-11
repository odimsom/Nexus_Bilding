using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class PostedDocsWithNoIncBufConfiguration : IEntityTypeConfiguration<PostedDocsWithNoIncBuf>
{
    public void Configure(EntityTypeBuilder<PostedDocsWithNoIncBuf> builder)
    {
        builder.ToTable("posted_docs_with_no_inc_buf", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.FirstPostingDescription).HasColumnName("first_posting_description");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.DebitAmount).HasColumnName("debit_amount").HasPrecision(18, 5);
        builder.Property(x => x.CreditAmount).HasColumnName("credit_amount").HasPrecision(18, 5);
    }
}
