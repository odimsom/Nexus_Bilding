using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ServiceRegisterConfiguration : IEntityTypeConfiguration<ServiceRegister>
{
    public void Configure(EntityTypeBuilder<ServiceRegister> builder)
    {
        builder.ToTable("service_register", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.FromEntryNo).HasColumnName("from_entry_no");
        builder.Property(x => x.ToEntryNo).HasColumnName("to_entry_no");
        builder.Property(x => x.FromWarrantyEntryNo).HasColumnName("from_warranty_entry_no");
        builder.Property(x => x.ToWarrantyEntryNo).HasColumnName("to_warranty_entry_no");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
    }
}
