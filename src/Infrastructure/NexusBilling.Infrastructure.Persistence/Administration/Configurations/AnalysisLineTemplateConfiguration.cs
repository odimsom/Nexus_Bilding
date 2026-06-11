using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AnalysisLineTemplateConfiguration : IEntityTypeConfiguration<AnalysisLineTemplate>
{
    public void Configure(EntityTypeBuilder<AnalysisLineTemplate> builder)
    {
        builder.ToTable("analysis_line_template", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AnalysisArea).HasColumnName("analysis_area");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.DefaultColumnTemplateName).HasColumnName("default_column_template_name");
        builder.Property(x => x.ItemAnalysisViewCode).HasColumnName("item_analysis_view_code");
    }
}
