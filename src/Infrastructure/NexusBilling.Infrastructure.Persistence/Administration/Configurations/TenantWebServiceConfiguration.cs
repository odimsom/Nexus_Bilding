using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TenantWebServiceConfiguration : IEntityTypeConfiguration<TenantWebService>
{
    public void Configure(EntityTypeBuilder<TenantWebService> builder)
    {
        builder.ToTable("tenant_web_service", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ObjectType).HasColumnName("object_type");
        builder.Property(x => x.ObjectId).HasColumnName("object_id");
        builder.Property(x => x.ServiceName).HasColumnName("service_name");
        builder.Property(x => x.Published).HasColumnName("published");
    }
}
