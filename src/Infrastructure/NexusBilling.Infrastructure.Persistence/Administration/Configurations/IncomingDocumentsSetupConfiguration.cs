using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class IncomingDocumentsSetupConfiguration : IEntityTypeConfiguration<IncomingDocumentsSetup>
{
    public void Configure(EntityTypeBuilder<IncomingDocumentsSetup> builder)
    {
        builder.ToTable("incoming_documents_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.GeneralJournalTemplateName).HasColumnName("general_journal_template_name");
        builder.Property(x => x.GeneralJournalBatchName).HasColumnName("general_journal_batch_name");
        builder.Property(x => x.RequireApprovalToCreate).HasColumnName("require_approval_to_create");
        builder.Property(x => x.RequireApprovalToPost).HasColumnName("require_approval_to_post");
    }
}
