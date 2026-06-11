using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class DynamicRequestPageFieldConfiguration : IEntityTypeConfiguration<DynamicRequestPageField>
{
    public void Configure(EntityTypeBuilder<DynamicRequestPageField> builder)
    {
        builder.ToTable("dynamic_request_page_field", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.FieldId).HasColumnName("field_id");
    }
}
