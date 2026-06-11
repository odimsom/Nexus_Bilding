using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ColumnLayoutConfiguration : IEntityTypeConfiguration<ColumnLayout>
{
    public void Configure(EntityTypeBuilder<ColumnLayout> builder)
    {
        builder.ToTable("column_layout", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ColumnLayoutName).HasColumnName("column_layout_name");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.ColumnNo).HasColumnName("column_no");
        builder.Property(x => x.ColumnHeader).HasColumnName("column_header");
        builder.Property(x => x.ColumnType).HasColumnName("column_type");
        builder.Property(x => x.LedgerEntryType).HasColumnName("ledger_entry_type");
        builder.Property(x => x.AmountType).HasColumnName("amount_type");
        builder.Property(x => x.Formula).HasColumnName("formula");
        builder.Property(x => x.ComparisonDateFormula).HasColumnName("comparison_date_formula");
        builder.Property(x => x.ShowOppositeSign).HasColumnName("show_opposite_sign");
        builder.Property(x => x.Show).HasColumnName("show");
        builder.Property(x => x.RoundingFactor).HasColumnName("rounding_factor");
        builder.Property(x => x.ShowIndentedLines).HasColumnName("show_indented_lines");
        builder.Property(x => x.ComparisonPeriodFormula).HasColumnName("comparison_period_formula");
        builder.Property(x => x.BusinessUnitTotaling).HasColumnName("business_unit_totaling");
        builder.Property(x => x.Dimension1Totaling).HasColumnName("dimension_1_totaling");
        builder.Property(x => x.Dimension2Totaling).HasColumnName("dimension_2_totaling");
        builder.Property(x => x.Dimension3Totaling).HasColumnName("dimension_3_totaling");
        builder.Property(x => x.Dimension4Totaling).HasColumnName("dimension_4_totaling");
        builder.Property(x => x.CostCenterTotaling).HasColumnName("cost_center_totaling");
        builder.Property(x => x.CostObjectTotaling).HasColumnName("cost_object_totaling");
        builder.Property(x => x.ComparisonPeriodFormulaLcid).HasColumnName("comparison_period_formula_lcid");
    }
}
