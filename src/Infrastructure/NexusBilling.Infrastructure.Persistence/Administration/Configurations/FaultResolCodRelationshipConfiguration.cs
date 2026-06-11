using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class FaultResolCodRelationshipConfiguration : IEntityTypeConfiguration<FaultResolCodRelationship>
{
    public void Configure(EntityTypeBuilder<FaultResolCodRelationship> builder)
    {
        builder.ToTable("fault_resol_cod_relationship", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FaultCode).HasColumnName("fault_code");
        builder.Property(x => x.SymptomCode).HasColumnName("symptom_code");
        builder.Property(x => x.FaultAreaCode).HasColumnName("fault_area_code");
        builder.Property(x => x.ResolutionCode).HasColumnName("resolution_code");
        builder.Property(x => x.Occurrences).HasColumnName("occurrences");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ServiceItemGroupCode).HasColumnName("service_item_group_code");
        builder.Property(x => x.CreatedManually).HasColumnName("created_manually");
    }
}
