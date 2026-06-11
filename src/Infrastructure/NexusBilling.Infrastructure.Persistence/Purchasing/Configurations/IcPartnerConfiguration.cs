using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class IcPartnerConfiguration : IEntityTypeConfiguration<IcPartner>
{
    public void Configure(EntityTypeBuilder<IcPartner> builder)
    {
        builder.ToTable("ic_partner", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.InboxType).HasColumnName("inbox_type");
        builder.Property(x => x.InboxDetails).HasColumnName("inbox_details");
        builder.Property(x => x.ReceivablesAccount).HasColumnName("receivables_account");
        builder.Property(x => x.PayablesAccount).HasColumnName("payables_account");
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.CustomerNo).HasColumnName("customer_no");
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.OutboundSalesItemNoType).HasColumnName("outbound_sales_item_no_type");
        builder.Property(x => x.OutboundPurchItemNoType).HasColumnName("outbound_purch_item_no_type");
        builder.Property(x => x.CostDistributionInLcy).HasColumnName("cost_distribution_in_lcy");
    }
}
