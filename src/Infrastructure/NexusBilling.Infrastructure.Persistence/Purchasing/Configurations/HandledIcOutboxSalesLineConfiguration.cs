using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class HandledIcOutboxSalesLineConfiguration : IEntityTypeConfiguration<HandledIcOutboxSalesLine>
{
    public void Configure(EntityTypeBuilder<HandledIcOutboxSalesLine> builder)
    {
        builder.ToTable("handled_ic_outbox_sales_line", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price").HasPrecision(18, 5);
        builder.Property(x => x.LineDiscount).HasColumnName("line_discount").HasPrecision(18, 5);
        builder.Property(x => x.LineDiscountAmount).HasColumnName("line_discount_amount").HasPrecision(18, 5);
        builder.Property(x => x.AmountIncludingVat).HasColumnName("amount_including_vat").HasPrecision(18, 5);
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.ShipmentNo).HasColumnName("shipment_no");
        builder.Property(x => x.ShipmentLineNo).HasColumnName("shipment_line_no");
        builder.Property(x => x.DropShipment).HasColumnName("drop_shipment");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.VatBaseAmount).HasColumnName("vat_base_amount").HasPrecision(18, 5);
        builder.Property(x => x.LineAmount).HasColumnName("line_amount").HasPrecision(18, 5);
        builder.Property(x => x.IcPartnerRefType).HasColumnName("ic_partner_ref_type");
        builder.Property(x => x.IcPartnerReference).HasColumnName("ic_partner_reference");
        builder.Property(x => x.IcPartnerCode).HasColumnName("ic_partner_code");
        builder.Property(x => x.IcTransactionNo).HasColumnName("ic_transaction_no");
        builder.Property(x => x.TransactionSource).HasColumnName("transaction_source");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.RequestedDeliveryDate).HasColumnName("requested_delivery_date");
        builder.Property(x => x.PromisedDeliveryDate).HasColumnName("promised_delivery_date");
        builder.Property(x => x.ReturnReceiptNo).HasColumnName("return_receipt_no");
        builder.Property(x => x.ReturnReceiptLineNo).HasColumnName("return_receipt_line_no");
    }
}
