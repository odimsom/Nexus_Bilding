using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class CommentLineConfiguration : IEntityTypeConfiguration<CommentLine>
{
    public void Configure(EntityTypeBuilder<CommentLine> builder)
    {
        builder.ToTable("comment_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableName).HasColumnName("table_name");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Comment).HasColumnName("comment");
    }
}
