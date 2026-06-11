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
    }
}
