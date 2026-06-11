using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigTemplateHeaderConfiguration : IEntityTypeConfiguration<ConfigTemplateHeader>
{
    public void Configure(EntityTypeBuilder<ConfigTemplateHeader> builder)
    {
        builder.ToTable("config_template_header", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.Enabled).HasColumnName("enabled");
        builder.Property(x => x.InstanceNoSeries).HasColumnName("instance_no_series");
    }
}
