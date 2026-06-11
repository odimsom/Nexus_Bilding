using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class CustomReportLayoutConfiguration : IEntityTypeConfiguration<CustomReportLayout>
{
    public void Configure(EntityTypeBuilder<CustomReportLayout> builder)
    {
        builder.ToTable("custom_report_layout", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.ReportId).HasColumnName("report_id");
        builder.Property(x => x.CompanyName).HasColumnName("company_name");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Layout).HasColumnName("layout");
        builder.Property(x => x.LastModified).HasColumnName("last_modified");
        builder.Property(x => x.LastModifiedByUser).HasColumnName("last_modified_by_user");
        builder.Property(x => x.FileExtension).HasColumnName("file_extension");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CustomXmlPart).HasColumnName("custom_xml_part");
        builder.Property(x => x.AppId).HasColumnName("app_id");
        builder.Property(x => x.BuiltIn).HasColumnName("built_in");
    }
}
