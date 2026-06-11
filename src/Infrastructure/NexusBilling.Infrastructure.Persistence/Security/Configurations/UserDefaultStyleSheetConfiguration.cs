using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class UserDefaultStyleSheetConfiguration : IEntityTypeConfiguration<UserDefaultStyleSheet>
{
    public void Configure(EntityTypeBuilder<UserDefaultStyleSheet> builder)
    {
        builder.ToTable("user_default_style_sheet", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ObjectType).HasColumnName("object_type");
        builder.Property(x => x.ObjectId).HasColumnName("object_id");
        builder.Property(x => x.ProgramId).HasColumnName("program_id");
        builder.Property(x => x.StyleSheetId).HasColumnName("style_sheet_id");
    }
}
