using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceCommentLineConfiguration : IEntityTypeConfiguration<ServiceCommentLine>
{
    public void Configure(EntityTypeBuilder<ServiceCommentLine> builder)
    {
        builder.ToTable("service_comment_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.TableLineNo).HasColumnName("table_line_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Comment).HasColumnName("comment");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.TableSubtype).HasColumnName("table_subtype");
        builder.Property(x => x.TableName).HasColumnName("table_name");
    }
}
