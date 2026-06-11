using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class IntrastatJnlTemplateConfiguration : IEntityTypeConfiguration<IntrastatJnlTemplate>
{
    public void Configure(EntityTypeBuilder<IntrastatJnlTemplate> builder)
    {
        builder.ToTable("intrastat_jnl_template", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ChecklistReportId).HasColumnName("checklist_report_id");
        builder.Property(x => x.PageId).HasColumnName("page_id");
    }
}
