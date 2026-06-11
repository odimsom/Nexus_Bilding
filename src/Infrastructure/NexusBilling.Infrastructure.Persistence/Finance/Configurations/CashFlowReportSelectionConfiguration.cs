using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CashFlowReportSelectionConfiguration : IEntityTypeConfiguration<CashFlowReportSelection>
{
    public void Configure(EntityTypeBuilder<CashFlowReportSelection> builder)
    {
        builder.ToTable("cash_flow_report_selection", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Sequence).HasColumnName("sequence");
        builder.Property(x => x.ReportId).HasColumnName("report_id");
    }
}
