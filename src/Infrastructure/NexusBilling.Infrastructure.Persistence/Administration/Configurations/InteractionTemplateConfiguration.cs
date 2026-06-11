using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class InteractionTemplateConfiguration : IEntityTypeConfiguration<InteractionTemplate>
{
    public void Configure(EntityTypeBuilder<InteractionTemplate> builder)
    {
        builder.ToTable("interaction_template", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.InteractionGroupCode).HasColumnName("interaction_group_code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.UnitCostLcy).HasColumnName("unit_cost_lcy").HasPrecision(18, 5);
        builder.Property(x => x.UnitDurationMin).HasColumnName("unit_duration_min").HasPrecision(18, 5);
        builder.Property(x => x.InformationFlow).HasColumnName("information_flow");
        builder.Property(x => x.InitiatedBy).HasColumnName("initiated_by");
        builder.Property(x => x.CampaignNo).HasColumnName("campaign_no");
        builder.Property(x => x.CampaignTarget).HasColumnName("campaign_target");
        builder.Property(x => x.CampaignResponse).HasColumnName("campaign_response");
        builder.Property(x => x.CorrespondenceTypeDefault).HasColumnName("correspondence_type_default");
        builder.Property(x => x.LanguageCodeDefault).HasColumnName("language_code_default");
        builder.Property(x => x.WizardAction).HasColumnName("wizard_action");
        builder.Property(x => x.IgnoreContactCorresType).HasColumnName("ignore_contact_corres_type");
    }
}
