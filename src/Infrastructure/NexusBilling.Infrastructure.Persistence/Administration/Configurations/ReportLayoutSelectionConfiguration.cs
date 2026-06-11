using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ReportLayoutSelectionConfiguration : IEntityTypeConfiguration<ReportLayoutSelection>
{
    public void Configure(EntityTypeBuilder<ReportLayoutSelection> builder)
    {
        builder.ToTable("report_layout_selection", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ReportId).HasColumnName("report_id");
        builder.Property(x => x.ReportName).HasColumnName("report_name");
        builder.Property(x => x.CompanyName).HasColumnName("company_name");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.CustomReportLayoutCode).HasColumnName("custom_report_layout_code");
    }
}
