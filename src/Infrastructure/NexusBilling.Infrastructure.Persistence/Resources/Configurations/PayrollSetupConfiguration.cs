using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class PayrollSetupConfiguration : IEntityTypeConfiguration<PayrollSetup>
{
    public void Configure(EntityTypeBuilder<PayrollSetup> builder)
    {
        builder.ToTable("payroll_setup", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.GeneralJournalTemplateName).HasColumnName("general_journal_template_name");
        builder.Property(x => x.GeneralJournalBatchName).HasColumnName("general_journal_batch_name");
        builder.Property(x => x.UserName).HasColumnName("user_name");
    }
}
