using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class TenantWebServiceOdataConfiguration : IEntityTypeConfiguration<TenantWebServiceOdata>
{
    public void Configure(EntityTypeBuilder<TenantWebServiceOdata> builder)
    {
        builder.ToTable("tenant_web_service_odata", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Tenantwebserviceid).HasColumnName("tenantwebserviceid");
        builder.Property(x => x.Odataselectclause).HasColumnName("odataselectclause");
        builder.Property(x => x.Odatafilterclause).HasColumnName("odatafilterclause");
    }
}
