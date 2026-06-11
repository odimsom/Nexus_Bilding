using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigTemplateLineConfiguration : IEntityTypeConfiguration<ConfigTemplateLine>
{
    public void Configure(EntityTypeBuilder<ConfigTemplateLine> builder)
    {
        builder.ToTable("config_template_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DataTemplateCode).HasColumnName("data_template_code");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.FieldId).HasColumnName("field_id");
        builder.Property(x => x.FieldName).HasColumnName("field_name");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.TemplateCode).HasColumnName("template_code");
        builder.Property(x => x.Mandatory).HasColumnName("mandatory");
        builder.Property(x => x.Reference).HasColumnName("reference");
        builder.Property(x => x.DefaultValue).HasColumnName("default_value");
        builder.Property(x => x.SkipRelationCheck).HasColumnName("skip_relation_check");
        builder.Property(x => x.LanguageId).HasColumnName("language_id");
    }
}
