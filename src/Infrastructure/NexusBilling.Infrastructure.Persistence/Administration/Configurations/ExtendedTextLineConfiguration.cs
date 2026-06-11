using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ExtendedTextLineConfiguration : IEntityTypeConfiguration<ExtendedTextLine>
{
    public void Configure(EntityTypeBuilder<ExtendedTextLine> builder)
    {
        builder.ToTable("extended_text_line", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.TableName).HasColumnName("table_name");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.TextNo).HasColumnName("text_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.Text).HasColumnName("text");
    }
}
