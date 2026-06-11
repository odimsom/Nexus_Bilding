using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ContactJobResponsibilityConfiguration : IEntityTypeConfiguration<ContactJobResponsibility>
{
    public void Configure(EntityTypeBuilder<ContactJobResponsibility> builder)
    {
        builder.ToTable("contact_job_responsibility", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ContactNo).HasColumnName("contact_no");
        builder.Property(x => x.JobResponsibilityCode).HasColumnName("job_responsibility_code");
    }
}
