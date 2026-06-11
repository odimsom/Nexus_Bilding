using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class ReqWkshTemplateConfiguration : IEntityTypeConfiguration<ReqWkshTemplate>
{
    public void Configure(EntityTypeBuilder<ReqWkshTemplate> builder)
    {
        builder.ToTable("req_wksh_template", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.PageId).HasColumnName("page_id");
        builder.Property(x => x.Recurring).HasColumnName("recurring");
        builder.Property(x => x.Type).HasColumnName("type");
    }
}
