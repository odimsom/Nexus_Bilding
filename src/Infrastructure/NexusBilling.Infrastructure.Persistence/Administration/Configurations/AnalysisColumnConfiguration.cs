using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AnalysisColumnConfiguration : IEntityTypeConfiguration<AnalysisColumn>
{
    public void Configure(EntityTypeBuilder<AnalysisColumn> builder)
    {
        builder.ToTable("analysis_column", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.AnalysisArea).HasColumnName("analysis_area");
        builder.Property(x => x.AnalysisColumnTemplate).HasColumnName("analysis_column_template");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.ColumnNo).HasColumnName("column_no");
        builder.Property(x => x.ColumnHeader).HasColumnName("column_header");
        builder.Property(x => x.ColumnType).HasColumnName("column_type");
        builder.Property(x => x.LedgerEntryType).HasColumnName("ledger_entry_type");
        builder.Property(x => x.Formula).HasColumnName("formula");
        builder.Property(x => x.ComparisonDateFormula).HasColumnName("comparison_date_formula");
        builder.Property(x => x.ShowOppositeSign).HasColumnName("show_opposite_sign");
        builder.Property(x => x.Show).HasColumnName("show");
        builder.Property(x => x.RoundingFactor).HasColumnName("rounding_factor");
        builder.Property(x => x.ComparisonPeriodFormula).HasColumnName("comparison_period_formula");
        builder.Property(x => x.AnalysisTypeCode).HasColumnName("analysis_type_code");
        builder.Property(x => x.ItemLedgerEntryTypeFilter).HasColumnName("item_ledger_entry_type_filter");
        builder.Property(x => x.ValueEntryTypeFilter).HasColumnName("value_entry_type_filter");
        builder.Property(x => x.ValueType).HasColumnName("value_type");
        builder.Property(x => x.Invoiced).HasColumnName("invoiced");
        builder.Property(x => x.ComparisonPeriodFormulaLcid).HasColumnName("comparison_period_formula_lcid");
    }
}
