using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class RecordLinkConfiguration : IEntityTypeConfiguration<RecordLink>
{
    public void Configure(EntityTypeBuilder<RecordLink> builder)
    {
        builder.ToTable("record_link", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.LinkId).HasColumnName("link_id");
        builder.Property(x => x.RecordId).HasColumnName("record_id");
        builder.Property(x => x.Url1).HasColumnName("url1");
        builder.Property(x => x.Url2).HasColumnName("url2");
        builder.Property(x => x.Url3).HasColumnName("url3");
        builder.Property(x => x.Url4).HasColumnName("url4");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Note).HasColumnName("note");
        builder.Property(x => x.Created).HasColumnName("created");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Company).HasColumnName("company");
        builder.Property(x => x.Notify).HasColumnName("notify");
        builder.Property(x => x.ToUserId).HasColumnName("to_user_id");
    }
}
