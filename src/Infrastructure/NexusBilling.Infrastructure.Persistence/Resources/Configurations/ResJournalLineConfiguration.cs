using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class ResJournalLineConfiguration : IEntityTypeConfiguration<ResJournalLine>
{
    public void Configure(EntityTypeBuilder<ResJournalLine> builder)
    {
        builder.ToTable("res_journal_line", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.EntryType).HasColumnName("entry_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.ResourceNo).HasColumnName("resource_no");
        builder.Property(x => x.ResourceGroupNo).HasColumnName("resource_group_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.WorkTypeCode).HasColumnName("work_type_code");
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.DirectUnitCost).HasColumnName("direct_unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.TotalCost).HasColumnName("total_cost").HasPrecision(18, 5);
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price").HasPrecision(18, 5);
        builder.Property(x => x.TotalPrice).HasColumnName("total_price").HasPrecision(18, 5);
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.RecurringMethod).HasColumnName("recurring_method");
        builder.Property(x => x.ExpirationDate).HasColumnName("expiration_date");
        builder.Property(x => x.RecurringFrequency).HasColumnName("recurring_frequency");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.PostingNoSeries).HasColumnName("posting_no_series");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.QtyPerUnitOfMeasure).HasColumnName("qty_per_unit_of_measure").HasPrecision(18, 5);
        builder.Property(x => x.OrderType).HasColumnName("order_type");
        builder.Property(x => x.OrderNo).HasColumnName("order_no");
        builder.Property(x => x.OrderLineNo).HasColumnName("order_line_no");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.TimeSheetNo).HasColumnName("time_sheet_no");
        builder.Property(x => x.TimeSheetLineNo).HasColumnName("time_sheet_line_no");
        builder.Property(x => x.TimeSheetDate).HasColumnName("time_sheet_date");
        builder.Property(x => x.SystemCreatedEntry).HasColumnName("system_created_entry");
    }
}
