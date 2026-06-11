using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class IcInboxPurchaseHeaderConfiguration : IEntityTypeConfiguration<IcInboxPurchaseHeader>
{
    public void Configure(EntityTypeBuilder<IcInboxPurchaseHeader> builder)
    {
        builder.ToTable("ic_inbox_purchase_header", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.BuyFromVendorNo).HasColumnName("buy_from_vendor_no");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.PayToVendorNo).HasColumnName("pay_to_vendor_no");
        builder.Property(x => x.YourReference).HasColumnName("your_reference");
        builder.Property(x => x.ShipToName).HasColumnName("ship_to_name");
        builder.Property(x => x.ShipToAddress).HasColumnName("ship_to_address");
        builder.Property(x => x.ShipToAddress2).HasColumnName("ship_to_address_2");
        builder.Property(x => x.ShipToCity).HasColumnName("ship_to_city");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.ExpectedReceiptDate).HasColumnName("expected_receipt_date");
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.PaymentDiscount).HasColumnName("payment_discount").HasPrecision(18, 5);
        builder.Property(x => x.PmtDiscountDate).HasColumnName("pmt_discount_date");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.PricesIncludingVat).HasColumnName("prices_including_vat");
        builder.Property(x => x.VendorOrderNo).HasColumnName("vendor_order_no");
        builder.Property(x => x.VendorInvoiceNo).HasColumnName("vendor_invoice_no");
        builder.Property(x => x.VendorCrMemoNo).HasColumnName("vendor_cr_memo_no");
        builder.Property(x => x.SellToCustomerNo).HasColumnName("sell_to_customer_no");
        builder.Property(x => x.ShipToPostCode).HasColumnName("ship_to_post_code");
        builder.Property(x => x.DocumentDate).HasColumnName("document_date");
        builder.Property(x => x.IcPartnerCode).HasColumnName("ic_partner_code");
        builder.Property(x => x.IcTransactionNo).HasColumnName("ic_transaction_no");
        builder.Property(x => x.TransactionSource).HasColumnName("transaction_source");
        builder.Property(x => x.RequestedReceiptDate).HasColumnName("requested_receipt_date");
        builder.Property(x => x.PromisedReceiptDate).HasColumnName("promised_receipt_date");
    }
}
