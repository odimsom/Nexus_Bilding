using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class PrinterSelectionConfiguration : IEntityTypeConfiguration<PrinterSelection>
{
    public void Configure(EntityTypeBuilder<PrinterSelection> builder)
    {
        builder.ToTable("printer_selection", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ReportId).HasColumnName("report_id");
        builder.Property(x => x.PrinterName).HasColumnName("printer_name");
    }
}
