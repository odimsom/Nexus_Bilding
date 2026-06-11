using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class AccScheduleNameConfiguration : IEntityTypeConfiguration<AccScheduleName>
{
    public void Configure(EntityTypeBuilder<AccScheduleName> builder)
    {
        builder.ToTable("acc_schedule_name", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.DefaultColumnLayout).HasColumnName("default_column_layout");
        builder.Property(x => x.AnalysisViewName).HasColumnName("analysis_view_name");
    }
}
