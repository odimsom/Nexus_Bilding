using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class PurchaseHeaderConfiguration : IEntityTypeConfiguration<PurchaseHeader>
{
    public void Configure(EntityTypeBuilder<PurchaseHeader> builder)
    {
        builder.ToTable("purchase_header", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.BuyFromVendorNo).HasColumnName("buy_from_vendor_no");
        builder.Property(x => x.PayToName).HasColumnName("pay_to_name");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.DueDate).HasColumnName("due_date");
        builder.Property(x => x.Status).HasColumnName("status").HasMaxLength(50);
        builder.Property(x => x.Amount).HasColumnName("amount").HasColumnType("decimal(18,2)");
        builder.Property(x => x.AmountIncludingVat).HasColumnName("amount_including_vat").HasColumnType("decimal(18,2)");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code").HasMaxLength(10);
        builder.Property(x => x.PaymentTermsCode).HasColumnName("payment_terms_code").HasMaxLength(20);
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no").HasMaxLength(50);
    }
}
