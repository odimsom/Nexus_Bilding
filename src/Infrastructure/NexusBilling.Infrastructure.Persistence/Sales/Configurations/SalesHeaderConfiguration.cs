using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class SalesHeaderConfiguration : IEntityTypeConfiguration<SalesHeader>
{
    public void Configure(EntityTypeBuilder<SalesHeader> builder)
    {
        builder.ToTable("sales_header", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.SellToCustomerNo).HasColumnName("sell_to_customer_no");
        builder.Property(x => x.BillToName).HasColumnName("bill_to_name");
        builder.Property(x => x.PostingDate).HasColumnName("posting_date");
        builder.Property(x => x.SellToCustomerName).HasColumnName("sell_to_customer_name").HasDefaultValue(string.Empty);
        builder.Property(x => x.DueDate).HasColumnName("due_date").IsRequired(false);
        builder.Property(x => x.Amount).HasColumnName("amount").HasDefaultValue(0m);
        builder.Property(x => x.AmountIncludingVat).HasColumnName("amount_including_vat").HasDefaultValue(0m);
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code").HasDefaultValue(string.Empty);
        builder.Property(x => x.PaymentTermsCode).HasColumnName("payment_terms_code").HasDefaultValue(string.Empty);
        builder.Property(x => x.PaymentMethodCode).HasColumnName("payment_method_code").HasDefaultValue(string.Empty);
        builder.Property(x => x.SalespersonCode).HasColumnName("salesperson_code").HasDefaultValue(string.Empty);
        builder.Property(x => x.ExternalDocumentNo).HasColumnName("external_document_no").HasDefaultValue(string.Empty);
        builder.Property(x => x.Status).HasColumnName("status").HasDefaultValue("Open");
    }
}
