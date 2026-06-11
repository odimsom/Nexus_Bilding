using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class ResourcesSetupConfiguration : IEntityTypeConfiguration<ResourcesSetup>
{
    public void Configure(EntityTypeBuilder<ResourcesSetup> builder)
    {
        builder.ToTable("resources_setup", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.ResourceNos).HasColumnName("resource_nos");
        builder.Property(x => x.TimeSheetNos).HasColumnName("time_sheet_nos");
        builder.Property(x => x.TimeSheetFirstWeekday).HasColumnName("time_sheet_first_weekday");
        builder.Property(x => x.TimeSheetByJobApproval).HasColumnName("time_sheet_by_job_approval");
    }
}
