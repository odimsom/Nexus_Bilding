using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class VatReportSetupConfiguration : IEntityTypeConfiguration<VatReportSetup>
{
    public void Configure(EntityTypeBuilder<VatReportSetup> builder)
    {
        builder.ToTable("vat_report_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.ModifySubmittedReports).HasColumnName("modify_submitted_reports");
    }
}
