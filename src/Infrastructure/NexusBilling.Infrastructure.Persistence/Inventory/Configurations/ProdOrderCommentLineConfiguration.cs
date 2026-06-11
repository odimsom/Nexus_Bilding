using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Inventory.Entities;

namespace NexusBilling.Infrastructure.Persistence.Inventory.Configurations;

public class ProdOrderCommentLineConfiguration : IEntityTypeConfiguration<ProdOrderCommentLine>
{
    public void Configure(EntityTypeBuilder<ProdOrderCommentLine> builder)
    {
        builder.ToTable("prod_order_comment_line", "inventory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.ProdOrderNo).HasColumnName("prod_order_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Comment).HasColumnName("comment");
    }
}
