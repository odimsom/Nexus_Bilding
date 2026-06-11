using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class GenericChartSetupConfiguration : IEntityTypeConfiguration<GenericChartSetup>
{
    public void Configure(EntityTypeBuilder<GenericChartSetup> builder)
    {
        builder.ToTable("generic_chart_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Title).HasColumnName("title");
        builder.Property(x => x.FilterText).HasColumnName("filter_text");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.SourceId).HasColumnName("source_id");
        builder.Property(x => x.ObjectName).HasColumnName("object_name");
        builder.Property(x => x.XAxisFieldId).HasColumnName("x_axis_field_id");
        builder.Property(x => x.XAxisFieldName).HasColumnName("x_axis_field_name");
        builder.Property(x => x.XAxisFieldCaption).HasColumnName("x_axis_field_caption");
        builder.Property(x => x.XAxisTitle).HasColumnName("x_axis_title");
        builder.Property(x => x.XAxisShowTitle).HasColumnName("x_axis_show_title");
        builder.Property(x => x.YAxisTitle).HasColumnName("y_axis_title");
        builder.Property(x => x.YAxisShowTitle).HasColumnName("y_axis_show_title");
        builder.Property(x => x.ZAxisFieldId).HasColumnName("z_axis_field_id");
        builder.Property(x => x.ZAxisFieldName).HasColumnName("z_axis_field_name");
        builder.Property(x => x.ZAxisFieldCaption).HasColumnName("z_axis_field_caption");
        builder.Property(x => x.ZAxisTitle).HasColumnName("z_axis_title");
        builder.Property(x => x.ZAxisShowTitle).HasColumnName("z_axis_show_title");
    }
}
