using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AccScheduleLineConfiguration : IEntityTypeConfiguration<AccScheduleLine>
{
    public void Configure(EntityTypeBuilder<AccScheduleLine> builder)
    {
        builder.ToTable("acc_schedule_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ScheduleName).HasColumnName("schedule_name");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.RowNo).HasColumnName("row_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Totaling).HasColumnName("totaling");
        builder.Property(x => x.TotalingType).HasColumnName("totaling_type");
        builder.Property(x => x.NewPage).HasColumnName("new_page");
        builder.Property(x => x.Indentation).HasColumnName("indentation");
        builder.Property(x => x.Show).HasColumnName("show");
        builder.Property(x => x.Dimension1Totaling).HasColumnName("dimension_1_totaling");
        builder.Property(x => x.Dimension2Totaling).HasColumnName("dimension_2_totaling");
        builder.Property(x => x.Dimension3Totaling).HasColumnName("dimension_3_totaling");
        builder.Property(x => x.Dimension4Totaling).HasColumnName("dimension_4_totaling");
        builder.Property(x => x.Bold).HasColumnName("bold");
        builder.Property(x => x.Italic).HasColumnName("italic");
        builder.Property(x => x.Underline).HasColumnName("underline");
        builder.Property(x => x.ShowOppositeSign).HasColumnName("show_opposite_sign");
        builder.Property(x => x.RowType).HasColumnName("row_type");
        builder.Property(x => x.AmountType).HasColumnName("amount_type");
        builder.Property(x => x.DoubleUnderline).HasColumnName("double_underline");
        builder.Property(x => x.CostCenterTotaling).HasColumnName("cost_center_totaling");
        builder.Property(x => x.CostObjectTotaling).HasColumnName("cost_object_totaling");
    }
}
