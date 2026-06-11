using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ExtendedTextHeaderConfiguration : IEntityTypeConfiguration<ExtendedTextHeader>
{
    public void Configure(EntityTypeBuilder<ExtendedTextHeader> builder)
    {
        builder.ToTable("extended_text_header", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableName).HasColumnName("table_name");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.TextNo).HasColumnName("text_no");
        builder.Property(x => x.StartingDate).HasColumnName("starting_date");
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.AllLanguageCodes).HasColumnName("all_language_codes");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.SalesQuote).HasColumnName("sales_quote");
        builder.Property(x => x.SalesInvoice).HasColumnName("sales_invoice");
        builder.Property(x => x.SalesOrder).HasColumnName("sales_order");
        builder.Property(x => x.SalesCreditMemo).HasColumnName("sales_credit_memo");
        builder.Property(x => x.PurchaseQuote).HasColumnName("purchase_quote");
        builder.Property(x => x.PurchaseInvoice).HasColumnName("purchase_invoice");
        builder.Property(x => x.PurchaseOrder).HasColumnName("purchase_order");
        builder.Property(x => x.PurchaseCreditMemo).HasColumnName("purchase_credit_memo");
        builder.Property(x => x.Reminder).HasColumnName("reminder");
        builder.Property(x => x.FinanceChargeMemo).HasColumnName("finance_charge_memo");
        builder.Property(x => x.SalesBlanketOrder).HasColumnName("sales_blanket_order");
        builder.Property(x => x.PurchaseBlanketOrder).HasColumnName("purchase_blanket_order");
        builder.Property(x => x.PrepmtSalesInvoice).HasColumnName("prepmt_sales_invoice");
        builder.Property(x => x.PrepmtSalesCreditMemo).HasColumnName("prepmt_sales_credit_memo");
        builder.Property(x => x.PrepmtPurchaseInvoice).HasColumnName("prepmt_purchase_invoice");
        builder.Property(x => x.PrepmtPurchaseCreditMemo).HasColumnName("prepmt_purchase_credit_memo");
        builder.Property(x => x.ServiceOrder).HasColumnName("service_order");
        builder.Property(x => x.ServiceQuote).HasColumnName("service_quote");
        builder.Property(x => x.ServiceInvoice).HasColumnName("service_invoice");
        builder.Property(x => x.ServiceCreditMemo).HasColumnName("service_credit_memo");
        builder.Property(x => x.SalesReturnOrder).HasColumnName("sales_return_order");
        builder.Property(x => x.PurchaseReturnOrder).HasColumnName("purchase_return_order");
    }
}
