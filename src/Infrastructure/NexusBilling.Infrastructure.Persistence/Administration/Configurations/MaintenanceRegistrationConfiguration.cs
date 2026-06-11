using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class MaintenanceRegistrationConfiguration : IEntityTypeConfiguration<MaintenanceRegistration>
{
    public void Configure(EntityTypeBuilder<MaintenanceRegistration> builder)
    {
        builder.ToTable("maintenance_registration", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FaNo).HasColumnName("fa_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.ServiceDate).HasColumnName("service_date");
        builder.Property(x => x.MaintenanceVendorNo).HasColumnName("maintenance_vendor_no");
        builder.Property(x => x.Comment).HasColumnName("comment");
        builder.Property(x => x.ServiceAgentName).HasColumnName("service_agent_name");
        builder.Property(x => x.ServiceAgentPhoneNo).HasColumnName("service_agent_phone_no");
        builder.Property(x => x.ServiceAgentMobilePhone).HasColumnName("service_agent_mobile_phone");
    }
}
