using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class O365GettingStartedPageDataConfiguration : IEntityTypeConfiguration<O365GettingStartedPageData>
{
    public void Configure(EntityTypeBuilder<O365GettingStartedPageData> builder)
    {
        builder.ToTable("o365_getting_started_page_data", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.DisplayTarget).HasColumnName("display_target");
        builder.Property(x => x.WizardId).HasColumnName("wizard_id");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Image).HasColumnName("image");
        builder.Property(x => x.BodyText).HasColumnName("body_text");
    }
}
