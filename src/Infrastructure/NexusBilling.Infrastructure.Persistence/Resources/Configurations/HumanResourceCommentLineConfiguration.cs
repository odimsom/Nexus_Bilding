using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class HumanResourceCommentLineConfiguration : IEntityTypeConfiguration<HumanResourceCommentLine>
{
    public void Configure(EntityTypeBuilder<HumanResourceCommentLine> builder)
    {
        builder.ToTable("human_resource_comment_line", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableName).HasColumnName("table_name");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.TableLineNo).HasColumnName("table_line_no");
        builder.Property(x => x.AlternativeAddressCode).HasColumnName("alternative_address_code");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Comment).HasColumnName("comment");
    }
}
