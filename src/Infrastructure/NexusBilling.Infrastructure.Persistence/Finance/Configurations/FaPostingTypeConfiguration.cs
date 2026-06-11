using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaPostingTypeConfiguration : IEntityTypeConfiguration<FaPostingType>
{
    public void Configure(EntityTypeBuilder<FaPostingType> builder)
    {
        builder.ToTable("fa_posting_type", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.FaPostingTypeNo).HasColumnName("fa_posting_type_no");
        builder.Property(x => x.FaPostingTypeName).HasColumnName("fa_posting_type_name");
        builder.Property(x => x.FaEntry).HasColumnName("fa_entry");
        builder.Property(x => x.GLEntry).HasColumnName("g_l_entry");
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
    }
}
