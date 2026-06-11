using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AnalysisReportNameConfiguration : IEntityTypeConfiguration<AnalysisReportName>
{
    public void Configure(EntityTypeBuilder<AnalysisReportName> builder)
    {
        builder.ToTable("analysis_report_name", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AnalysisArea).HasColumnName("analysis_area");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.AnalysisLineTemplateName).HasColumnName("analysis_line_template_name");
        builder.Property(x => x.AnalysisColumnTemplateName).HasColumnName("analysis_column_template_name");
    }
}
