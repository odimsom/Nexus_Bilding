using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class ActivitiesCueConfiguration : IEntityTypeConfiguration<ActivitiesCue>
{
    public void Configure(EntityTypeBuilder<ActivitiesCue> builder)
    {
        builder.ToTable("activities_cue", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.SalesThisMonth).HasColumnName("sales_this_month").HasPrecision(18, 5);
        builder.Property(x => x.Top10CustomerSalesYtd).HasColumnName("top_10_customer_sales_ytd").HasPrecision(18, 5);
        builder.Property(x => x.OverduePurchInvoiceAmount).HasColumnName("overdue_purch_invoice_amount").HasPrecision(18, 5);
        builder.Property(x => x.OverdueSalesInvoiceAmount).HasColumnName("overdue_sales_invoice_amount").HasPrecision(18, 5);
        builder.Property(x => x.AverageCollectionDays).HasColumnName("average_collection_days").HasPrecision(18, 5);
    }
}
