using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class O365SalesInitialSetupConfiguration : IEntityTypeConfiguration<O365SalesInitialSetup>
{
    public void Configure(EntityTypeBuilder<O365SalesInitialSetup> builder)
    {
        builder.ToTable("o365_sales_initial_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.PaymentRegTemplateName).HasColumnName("payment_reg_template_name");
        builder.Property(x => x.PaymentRegBatchName).HasColumnName("payment_reg_batch_name");
        builder.Property(x => x.IsInitialized).HasColumnName("is_initialized");
        builder.Property(x => x.DefaultCustomerTemplate).HasColumnName("default_customer_template");
        builder.Property(x => x.DefaultItemTemplate).HasColumnName("default_item_template");
        builder.Property(x => x.DefaultPaymentTermsCode).HasColumnName("default_payment_terms_code");
        builder.Property(x => x.DefaultPaymentMethodCode).HasColumnName("default_payment_method_code");
        builder.Property(x => x.SalesInvoiceNoSeries).HasColumnName("sales_invoice_no_series");
        builder.Property(x => x.PostedSalesInvNoSeries).HasColumnName("posted_sales_inv_no_series");
    }
}
