using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigTableProcessingRuleConfiguration : IEntityTypeConfiguration<ConfigTableProcessingRule>
{
    public void Configure(EntityTypeBuilder<ConfigTableProcessingRule> builder)
    {
        builder.ToTable("config_table_processing_rule", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PackageCode).HasColumnName("package_code");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.RuleNo).HasColumnName("rule_no");
        builder.Property(x => x.Action).HasColumnName("action");
        builder.Property(x => x.CustomProcessingCodeunitId).HasColumnName("custom_processing_codeunit_id");
    }
}
