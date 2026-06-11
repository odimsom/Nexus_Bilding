using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class FaMatrixPostingTypeConfiguration : IEntityTypeConfiguration<FaMatrixPostingType>
{
    public void Configure(EntityTypeBuilder<FaMatrixPostingType> builder)
    {
        builder.ToTable("fa_matrix_posting_type", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.FaPostingTypeName).HasColumnName("fa_posting_type_name");
    }
}
