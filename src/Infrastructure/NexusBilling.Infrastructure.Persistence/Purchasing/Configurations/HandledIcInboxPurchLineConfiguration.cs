using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class HandledIcInboxPurchLineConfiguration : IEntityTypeConfiguration<HandledIcInboxPurchLine>
{
    public void Configure(EntityTypeBuilder<HandledIcInboxPurchLine> builder)
    {
        builder.ToTable("handled_ic_inbox_purch_line", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Quantity).HasColumnName("quantity").HasPrecision(18, 5);
        builder.Property(x => x.DirectUnitCost).HasColumnName("direct_unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.LineDiscount).HasColumnName("line_discount").HasPrecision(18, 5);
        builder.Property(x => x.LineDiscountAmount).HasColumnName("line_discount_amount").HasPrecision(18, 5);
        builder.Property(x => x.AmountIncludingVat).HasColumnName("amount_including_vat").HasPrecision(18, 5);
        builder.Property(x => x.JobNo).HasColumnName("job_no");
        builder.Property(x => x.IndirectCost).HasColumnName("indirect_cost").HasPrecision(18, 5);
        builder.Property(x => x.ReceiptNo).HasColumnName("receipt_no");
        builder.Property(x => x.ReceiptLineNo).HasColumnName("receipt_line_no");
        builder.Property(x => x.DropShipment).HasColumnName("drop_shipment");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.VatBaseAmount).HasColumnName("vat_base_amount").HasPrecision(18, 5);
        builder.Property(x => x.UnitCost).HasColumnName("unit_cost").HasPrecision(18, 5);
        builder.Property(x => x.LineAmount).HasColumnName("line_amount").HasPrecision(18, 5);
        builder.Property(x => x.IcPartnerRefType).HasColumnName("ic_partner_ref_type");
        builder.Property(x => x.IcPartnerReference).HasColumnName("ic_partner_reference");
        builder.Property(x => x.IcPartnerCode).HasColumnName("ic_partner_code");
        builder.Property(x => x.IcTransactionNo).HasColumnName("ic_transaction_no");
        builder.Property(x => x.TransactionSource).HasColumnName("transaction_source");
        builder.Property(x => x.UnitOfMeasureCode).HasColumnName("unit_of_measure_code");
        builder.Property(x => x.RequestedReceiptDate).HasColumnName("requested_receipt_date");
        builder.Property(x => x.PromisedReceiptDate).HasColumnName("promised_receipt_date");
        builder.Property(x => x.ReturnShipmentNo).HasColumnName("return_shipment_no");
        builder.Property(x => x.ReturnShipmentLineNo).HasColumnName("return_shipment_line_no");
    }
}
