using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class SalesCommentLineConfiguration : IEntityTypeConfiguration<SalesCommentLine>
{
    public void Configure(EntityTypeBuilder<SalesCommentLine> builder)
    {
        builder.ToTable("sales_comment_line", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Comment).HasColumnName("comment");
        builder.Property(x => x.DocumentLineNo).HasColumnName("document_line_no");
    }
}
