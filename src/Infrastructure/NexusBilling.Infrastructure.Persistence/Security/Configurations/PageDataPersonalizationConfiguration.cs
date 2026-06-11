using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Security.Entities;

namespace NexusBilling.Infrastructure.Persistence.Security.Configurations;

public class PageDataPersonalizationConfiguration : IEntityTypeConfiguration<PageDataPersonalization>
{
    public void Configure(EntityTypeBuilder<PageDataPersonalization> builder)
    {
        builder.ToTable("page_data_personalization", "security");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserSid).HasColumnName("user_sid");
        builder.Property(x => x.ObjectType).HasColumnName("object_type");
        builder.Property(x => x.ObjectId).HasColumnName("object_id");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.Time).HasColumnName("time");
        builder.Property(x => x.PersonalizationId).HasColumnName("personalization_id");
        builder.Property(x => x.Valuename).HasColumnName("valuename");
        builder.Property(x => x.Value).HasColumnName("value");
    }
}
