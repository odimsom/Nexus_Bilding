using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class PaymentRegistrationSetupConfiguration : IEntityTypeConfiguration<PaymentRegistrationSetup>
{
    public void Configure(EntityTypeBuilder<PaymentRegistrationSetup> builder)
    {
        builder.ToTable("payment_registration_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.JournalTemplateName).HasColumnName("journal_template_name");
        builder.Property(x => x.JournalBatchName).HasColumnName("journal_batch_name");
        builder.Property(x => x.BalAccountType).HasColumnName("bal_account_type");
        builder.Property(x => x.BalAccountNo).HasColumnName("bal_account_no");
        builder.Property(x => x.UseThisAccountAsDef).HasColumnName("use_this_account_as_def");
        builder.Property(x => x.AutoFillDateReceived).HasColumnName("auto_fill_date_received");
    }
}
