using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GLEntryConfiguration : IEntityTypeConfiguration<GLEntry>
{
    public void Configure(EntityTypeBuilder<GLEntry> builder)
    {
        builder.ToTable("gl_entry", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.GLAccountNo).HasColumnName("gl_account_no");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.BalAccountNo).HasColumnName("bal_account_no");
        builder.Property(x => x.Amount).HasColumnName("amount");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension2_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.SystemCreatedEntry).HasColumnName("system_created_entry");
        builder.Property(x => x.PriorYearEntry).HasColumnName("prior_year_entry");
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.Quantity).HasColumnName("quantity");
        builder.Property(x => x.VatAmount).HasColumnName("vat_amount");
        builder.Property(x => x.BusinessUnitCode).HasColumnName("business_unit_code");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.GenPostingType).HasColumnName("gen_posting_type");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.BalAccountType).HasColumnName("bal_account_type");
        builder.Property(x => x.TransactionNo).HasColumnName("transaction_no");
        builder.Property(x => x.DebitAmount).HasColumnName("debit_amount");
        builder.Property(x => x.CreditAmount).HasColumnName("credit_amount");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.TaxAreaCode).HasColumnName("tax_area_code");
        builder.Property(x => x.TaxLiable).HasColumnName("tax_liable");
        builder.Property(x => x.TaxGroupCode).HasColumnName("tax_group_code");
        builder.Property(x => x.UseTax).HasColumnName("use_tax");
        builder.Property(x => x.VatBusPostingGroup).HasColumnName("vat_bus_posting_group");
        builder.Property(x => x.VatProdPostingGroup).HasColumnName("vat_prod_posting_group");
        builder.Property(x => x.AdditionalCurrencyAmount).HasColumnName("additional_currency_amount");
        builder.Property(x => x.AddCurrencyDebitAmount).HasColumnName("add_currency_debit_amount");
        builder.Property(x => x.AddCurrencyCreditAmount).HasColumnName("add_currency_credit_amount");
        builder.Property(x => x.CloseIncomeStatementDimId).HasColumnName("close_income_statement_dim_id");
        builder.Property(x => x.IcPartnerCode).HasColumnName("ic_partner_code");
        builder.Property(x => x.Reversed).HasColumnName("reversed");
        builder.Property(x => x.ReversedByEntryNo).HasColumnName("reversed_by_entry_no");
        builder.Property(x => x.ReversedEntryNo).HasColumnName("reversed_entry_no");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.ProdOrderNo).HasColumnName("prod_order_no");
        builder.Property(x => x.FaEntryType).HasColumnName("fa_entry_type");
        builder.Property(x => x.FaEntryNo).HasColumnName("fa_entry_no");
    }
}
