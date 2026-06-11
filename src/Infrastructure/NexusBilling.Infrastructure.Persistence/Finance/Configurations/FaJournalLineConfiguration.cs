using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaJournalLineConfiguration : IEntityTypeConfiguration<FaJournalLine>
{
    public void Configure(EntityTypeBuilder<FaJournalLine> builder)
    {
        builder.ToTable("fa_journal_line", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.DepreciationBookCode).HasColumnName("depreciation_book_code");
        builder.Property(x => x.FaPostingType).HasColumnName("fa_posting_type");
        builder.Property(x => x.FaNo).HasColumnName("fa_no");
        builder.Property(x => x.FaPostingDate).HasColumnName("fa_posting_date");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.DebitAmount).HasColumnName("debit_amount").HasPrecision(18, 5);
        builder.Property(x => x.CreditAmount).HasColumnName("credit_amount").HasPrecision(18, 5);
        builder.Property(x => x.SalvageValue).HasColumnName("salvage_value").HasPrecision(18, 5);
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.Correction).HasColumnName("correction");
        builder.Property(x => x.NoOfDepreciationDays).HasColumnName("no_of_depreciation_days");
        builder.Property(x => x.DeprUntilFaPostingDate).HasColumnName("depr_until_fa_posting_date");
        builder.Property(x => x.DeprAcquisitionCost).HasColumnName("depr_acquisition_cost");
        builder.Property(x => x.FaPostingGroup).HasColumnName("fa_posting_group");
        builder.Property(x => x.MaintenanceCode).HasColumnName("maintenance_code");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension_1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension_2_code");
        builder.Property(x => x.InsuranceNo).HasColumnName("insurance_no");
        builder.Property(x => x.BudgetedFaNo).HasColumnName("budgeted_fa_no");
        builder.Property(x => x.UseDuplicationList).HasColumnName("use_duplication_list");
        builder.Property(x => x.DuplicateInDepreciationBook).HasColumnName("duplicate_in_depreciation_book");
        builder.Property(x => x.FaReclassificationEntry).HasColumnName("fa_reclassification_entry");
        builder.Property(x => x.FaErrorEntryNo).HasColumnName("fa_error_entry_no");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.RecurringMethod).HasColumnName("recurring_method");
        builder.Property(x => x.RecurringFrequency).HasColumnName("recurring_frequency");
        builder.Property(x => x.ExpirationDate).HasColumnName("expiration_date");
        builder.Property(x => x.IndexEntry).HasColumnName("index_entry");
        builder.Property(x => x.PostingNoSeries).HasColumnName("posting_no_series");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
