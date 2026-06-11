using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class MaintenanceLedgerEntryConfiguration : IEntityTypeConfiguration<MaintenanceLedgerEntry>
{
    public void Configure(EntityTypeBuilder<MaintenanceLedgerEntry> builder)
    {
        builder.ToTable("maintenance_ledger_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.GLEntryNo).HasColumnName("g_l_entry_no");
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
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.FaNoBudgetedFaNo).HasColumnName("fa_no_budgeted_fa_no");
        builder.Property(x => x.FaSubclassCode).HasColumnName("fa_subclass_code");
        builder.Property(x => x.FaLocationCode).HasColumnName("fa_location_code");
        builder.Property(x => x.FaPostingGroup).HasColumnName("fa_posting_group");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.LocationCode).HasColumnName("location_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.TransactionNo).HasColumnName("transaction_no");
        builder.Property(x => x.BalAccountType).HasColumnName("bal_account_type");
        builder.Property(x => x.BalAccountNo).HasColumnName("bal_account_no");
        builder.Property(x => x.VatAmount).HasColumnName("vat_amount").HasPrecision(18, 5);
        builder.Property(x => x.GenPostingType).HasColumnName("gen_posting_type");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.FaClassCode).HasColumnName("fa_class_code");
        builder.Property(x => x.DepreciationBookCode).HasColumnName("depreciation_book_code");
        builder.Property(x => x.FaExchangeRate).HasColumnName("fa_exchange_rate").HasPrecision(18, 5);
        builder.Property(x => x.AmountLcy).HasColumnName("amount_lcy").HasPrecision(18, 5);
        builder.Property(x => x.MaintenanceCode).HasColumnName("maintenance_code");
        builder.Property(x => x.Correction).HasColumnName("correction");
        builder.Property(x => x.IndexEntry).HasColumnName("index_entry");
        builder.Property(x => x.AutomaticEntry).HasColumnName("automatic_entry");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.TaxAreaCode).HasColumnName("tax_area_code");
        builder.Property(x => x.TaxLiable).HasColumnName("tax_liable");
        builder.Property(x => x.TaxGroupCode).HasColumnName("tax_group_code");
        builder.Property(x => x.UseTax).HasColumnName("use_tax");
        builder.Property(x => x.VatBusPostingGroup).HasColumnName("vat_bus_posting_group");
        builder.Property(x => x.VatProdPostingGroup).HasColumnName("vat_prod_posting_group");
        builder.Property(x => x.Reversed).HasColumnName("reversed");
        builder.Property(x => x.ReversedByEntryNo).HasColumnName("reversed_by_entry_no");
        builder.Property(x => x.ReversedEntryNo).HasColumnName("reversed_entry_no");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
    }
}
