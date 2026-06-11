using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class PaymentServiceSetupConfiguration : IEntityTypeConfiguration<PaymentServiceSetup>
{
    public void Configure(EntityTypeBuilder<PaymentServiceSetup> builder)
    {
        builder.ToTable("payment_service_setup", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Enabled).HasColumnName("enabled");
        builder.Property(x => x.AlwaysIncludeOnDocuments).HasColumnName("always_include_on_documents");
        builder.Property(x => x.SetupRecordId).HasColumnName("setup_record_id");
        builder.Property(x => x.SetupPageId).HasColumnName("setup_page_id");
        builder.Property(x => x.TermsOfService).HasColumnName("terms_of_service");
        builder.Property(x => x.Available).HasColumnName("available");
        builder.Property(x => x.ManagementCodeunitId).HasColumnName("management_codeunit_id");
    }
}
