using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaSubclassConfiguration : IEntityTypeConfiguration<FaSubclass>
{
    public void Configure(EntityTypeBuilder<FaSubclass> builder)
    {
        builder.ToTable("fa_subclass", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.FaClassCode).HasColumnName("fa_class_code");
        builder.Property(x => x.DefaultFaPostingGroup).HasColumnName("default_fa_posting_group");
    }
}
