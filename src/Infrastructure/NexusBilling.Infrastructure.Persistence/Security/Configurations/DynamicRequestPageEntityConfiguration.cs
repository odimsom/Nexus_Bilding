using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class DynamicRequestPageEntityConfiguration : IEntityTypeConfiguration<DynamicRequestPageEntity>
{
    public void Configure(EntityTypeBuilder<DynamicRequestPageEntity> builder)
    {
        builder.ToTable("dynamic_request_page_entity", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.RelatedTableId).HasColumnName("related_table_id");
        builder.Property(x => x.SequenceNo).HasColumnName("sequence_no");
    }
}
