using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class GLRegisterConfiguration : IEntityTypeConfiguration<GLRegister>
{
    public void Configure(EntityTypeBuilder<GLRegister> builder)
    {
        builder.ToTable("gl_register", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.FromEntryNo).HasColumnName("from_entry_no");
        builder.Property(x => x.ToEntryNo).HasColumnName("to_entry_no");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.SourceCode).HasColumnName("source_code");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.FromVatEntryNo).HasColumnName("from_vat_entry_no");
        builder.Property(x => x.ToVatEntryNo).HasColumnName("to_vat_entry_no");
        builder.Property(x => x.Reversed).HasColumnName("reversed");
    }
}
