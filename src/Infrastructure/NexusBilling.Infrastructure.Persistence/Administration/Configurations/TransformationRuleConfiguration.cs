using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TransformationRuleConfiguration : IEntityTypeConfiguration<TransformationRule>
{
    public void Configure(EntityTypeBuilder<TransformationRule> builder)
    {
        builder.ToTable("transformation_rule", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.TransformationType).HasColumnName("transformation_type");
        builder.Property(x => x.FindValue).HasColumnName("find_value");
        builder.Property(x => x.ReplaceValue).HasColumnName("replace_value");
        builder.Property(x => x.StartingText).HasColumnName("starting_text");
        builder.Property(x => x.EndingText).HasColumnName("ending_text");
        builder.Property(x => x.StartPosition).HasColumnName("start_position");
        builder.Property(x => x.Length).HasColumnName("length");
        builder.Property(x => x.DataFormat).HasColumnName("data_format");
        builder.Property(x => x.DataFormattingCulture).HasColumnName("data_formatting_culture");
        builder.Property(x => x.NextTransformationRule).HasColumnName("next_transformation_rule");
    }
}
