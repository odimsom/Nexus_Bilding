using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class StandardGeneralJournalLineConfiguration : IEntityTypeConfiguration<StandardGeneralJournalLine>
{
    public void Configure(EntityTypeBuilder<StandardGeneralJournalLine> builder)
    {
        builder.ToTable("standard_general_journal_line", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.AccountType).HasColumnName("account_type");
        builder.Property(x => x.AccountNo).HasColumnName("account_no");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Vat).HasColumnName("vat");
        builder.Property(x => x.BalAccountNo).HasColumnName("bal_account_no");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.Amount).HasColumnName("amount");
        builder.Property(x => x.DebitAmount).HasColumnName("debit_amount");
        builder.Property(x => x.CreditAmount).HasColumnName("credit_amount");
        builder.Property(x => x.AmountLcy).HasColumnName("amount_lcy");
        builder.Property(x => x.BalanceLcy).HasColumnName("balance_lcy");
        builder.Property(x => x.CurrencyFactor).HasColumnName("currency_factor");
        builder.Property(x => x.SalesPurchLcy).HasColumnName("sales_purch_lcy");
        builder.Property(x => x.ProfitLcy).HasColumnName("profit_lcy");
        builder.Property(x => x.InvDiscountLcy).HasColumnName("inv_discount_lcy");
        builder.Property(x => x.BillToPayToNo).HasColumnName("bill_to_pay_to_no");
        builder.Property(x => x.PostingGroup).HasColumnName("posting_group");
        builder.Property(x => x.ShortcutDimension1Code).HasColumnName("shortcut_dimension1_code");
        builder.Property(x => x.ShortcutDimension2Code).HasColumnName("shortcut_dimension2_code");
        builder.Property(x => x.SalespersPurchCode).HasColumnName("salespers_purch_code");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.OnHold).HasColumnName("on_hold");
        builder.Property(x => x.AppliesToDocType).HasColumnName("applies_to_doc_type");
        builder.Property(x => x.PaymentDiscount).HasColumnName("payment_discount");
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.Quantity).HasColumnName("quantity");
        builder.Property(x => x.VatAmount).HasColumnName("vat_amount");
        builder.Property(x => x.PaymentTermsCode).HasColumnName("payment_terms_code");
        builder.Property(x => x.BusinessUnitCode).HasColumnName("business_unit_code");
        builder.Property(x => x.StandardJournalCode).HasColumnName("standard_journal_code");
        builder.Property(x => x.ReasonCode).HasColumnName("reason_code");
        builder.Property(x => x.GenPostingType).HasColumnName("gen_posting_type");
        builder.Property(x => x.GenBusPostingGroup).HasColumnName("gen_bus_posting_group");
        builder.Property(x => x.GenProdPostingGroup).HasColumnName("gen_prod_posting_group");
        builder.Property(x => x.VatCalculationType).HasColumnName("vat_calculation_type");
        builder.Property(x => x.BalAccountType).HasColumnName("bal_account_type");
        builder.Property(x => x.BalGenPostingType).HasColumnName("bal_gen_posting_type");
        builder.Property(x => x.BalGenBusPostingGroup).HasColumnName("bal_gen_bus_posting_group");
        builder.Property(x => x.BalGenProdPostingGroup).HasColumnName("bal_gen_prod_posting_group");
        builder.Property(x => x.BalVatCalculationType).HasColumnName("bal_vat_calculation_type");
        builder.Property(x => x.BalVat).HasColumnName("bal_vat");
        builder.Property(x => x.BalVatAmount).HasColumnName("bal_vat_amount");
        builder.Property(x => x.BankPaymentType).HasColumnName("bank_payment_type");
        builder.Property(x => x.VatBaseAmount).HasColumnName("vat_base_amount");
        builder.Property(x => x.BalVatBaseAmount).HasColumnName("bal_vat_base_amount");
        builder.Property(x => x.Correction).HasColumnName("correction");
        builder.Property(x => x.SourceType).HasColumnName("source_type");
        builder.Property(x => x.SourceNo).HasColumnName("source_no");
        builder.Property(x => x.PostingNoSeries).HasColumnName("posting_no_series");
        builder.Property(x => x.TaxAreaCode).HasColumnName("tax_area_code");
        builder.Property(x => x.TaxLiable).HasColumnName("tax_liable");
        builder.Property(x => x.TaxGroupCode).HasColumnName("tax_group_code");
        builder.Property(x => x.UseTax).HasColumnName("use_tax");
        builder.Property(x => x.BalTaxAreaCode).HasColumnName("bal_tax_area_code");
        builder.Property(x => x.BalTaxLiable).HasColumnName("bal_tax_liable");
        builder.Property(x => x.BalTaxGroupCode).HasColumnName("bal_tax_group_code");
        builder.Property(x => x.BalUseTax).HasColumnName("bal_use_tax");
        builder.Property(x => x.VatBusPostingGroup).HasColumnName("vat_bus_posting_group");
        builder.Property(x => x.VatProdPostingGroup).HasColumnName("vat_prod_posting_group");
        builder.Property(x => x.BalVatBusPostingGroup).HasColumnName("bal_vat_bus_posting_group");
        builder.Property(x => x.BalVatProdPostingGroup).HasColumnName("bal_vat_prod_posting_group");
        builder.Property(x => x.ShipToOrderAddressCode).HasColumnName("ship_to_order_address_code");
        builder.Property(x => x.VatDifference).HasColumnName("vat_difference");
        builder.Property(x => x.BalVatDifference).HasColumnName("bal_vat_difference");
        builder.Property(x => x.IcPartnerCode).HasColumnName("ic_partner_code");
        builder.Property(x => x.IcPartnerGLAccNo).HasColumnName("ic_partner_gl_acc_no");
        builder.Property(x => x.SellToBuyFromNo).HasColumnName("sell_to_buy_from_no");
        builder.Property(x => x.DimensionSetId).HasColumnName("dimension_set_id");
        builder.Property(x => x.CampaignNo).HasColumnName("campaign_no");
        builder.Property(x => x.IndexEntry).HasColumnName("index_entry");
    }
}
