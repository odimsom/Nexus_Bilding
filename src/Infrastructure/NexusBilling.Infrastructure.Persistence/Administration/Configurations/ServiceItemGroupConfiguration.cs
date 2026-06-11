using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceItemGroupConfiguration : IEntityTypeConfiguration<ServiceItemGroup>
{
    public void Configure(EntityTypeBuilder<ServiceItemGroup> builder)
    {
        builder.ToTable("service_item_group", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CreateServiceItem).HasColumnName("create_service_item");
        builder.Property(x => x.DefaultContractDiscount).HasColumnName("default_contract_discount").HasPrecision(18, 5);
        builder.Property(x => x.DefaultServPriceGroupCode).HasColumnName("default_serv_price_group_code");
        builder.Property(x => x.DefaultResponseTimeHours).HasColumnName("default_response_time_hours").HasPrecision(18, 5);
    }
}
