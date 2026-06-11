using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AnalysisLineConfiguration : IEntityTypeConfiguration<AnalysisLine>
{
    public void Configure(EntityTypeBuilder<AnalysisLine> builder)
    {
        builder.ToTable("analysis_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AnalysisArea).HasColumnName("analysis_area");
        builder.Property(x => x.AnalysisLineTemplateName).HasColumnName("analysis_line_template_name");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.RowRefNo).HasColumnName("row_ref_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Range).HasColumnName("range");
        builder.Property(x => x.NewPage).HasColumnName("new_page");
        builder.Property(x => x.Show).HasColumnName("show");
        builder.Property(x => x.Bold).HasColumnName("bold");
        builder.Property(x => x.Italic).HasColumnName("italic");
        builder.Property(x => x.Underline).HasColumnName("underline");
        builder.Property(x => x.ShowOppositeSign).HasColumnName("show_opposite_sign");
        builder.Property(x => x.Dimension1Totaling).HasColumnName("dimension_1_totaling");
        builder.Property(x => x.Dimension2Totaling).HasColumnName("dimension_2_totaling");
        builder.Property(x => x.Dimension3Totaling).HasColumnName("dimension_3_totaling");
        builder.Property(x => x.GroupDimensionCode).HasColumnName("group_dimension_code");
        builder.Property(x => x.Indentation).HasColumnName("indentation");
    }
}
