using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ReportSelectionsConfiguration : IEntityTypeConfiguration<ReportSelections>
{
    public void Configure(EntityTypeBuilder<ReportSelections> builder)
    {
        builder.ToTable("report_selections", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Usage).HasColumnName("usage");
        builder.Property(x => x.Sequence).HasColumnName("sequence");
        builder.Property(x => x.ReportId).HasColumnName("report_id");
        builder.Property(x => x.CustomReportLayoutCode).HasColumnName("custom_report_layout_code");
        builder.Property(x => x.UseForEmailAttachment).HasColumnName("use_for_email_attachment");
        builder.Property(x => x.UseForEmailBody).HasColumnName("use_for_email_body");
        builder.Property(x => x.EmailBodyLayoutCode).HasColumnName("email_body_layout_code");
    }
}
