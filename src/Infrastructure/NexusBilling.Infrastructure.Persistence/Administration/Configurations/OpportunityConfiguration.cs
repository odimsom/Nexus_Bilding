using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class OpportunityConfiguration : IEntityTypeConfiguration<Opportunity>
{
    public void Configure(EntityTypeBuilder<Opportunity> builder)
    {
        builder.ToTable("opportunity", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.SalespersonCode).HasColumnName("salesperson_code");
        builder.Property(x => x.CampaignNo).HasColumnName("campaign_no");
        builder.Property(x => x.ContactNo).HasColumnName("contact_no");
        builder.Property(x => x.ContactCompanyNo).HasColumnName("contact_company_no");
        builder.Property(x => x.SalesCycleCode).HasColumnName("sales_cycle_code");
        builder.Property(x => x.SalesDocumentNo).HasColumnName("sales_document_no");
        builder.Property(x => x.CreationDate).HasColumnName("creation_date");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.Priority).HasColumnName("priority");
        builder.Property(x => x.Closed).HasColumnName("closed");
        builder.Property(x => x.DateClosed).HasColumnName("date_closed");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.SegmentNo).HasColumnName("segment_no");
        builder.Property(x => x.SalesDocumentType).HasColumnName("sales_document_type");
        builder.Property(x => x.WizardStep).HasColumnName("wizard_step");
        builder.Property(x => x.ActivateFirstStage).HasColumnName("activate_first_stage");
        builder.Property(x => x.SegmentDescription).HasColumnName("segment_description");
        builder.Property(x => x.WizardEstimatedValueLcy).HasColumnName("wizard_estimated_value_lcy").HasPrecision(18, 5);
        builder.Property(x => x.WizardChancesOfSuccess).HasColumnName("wizard_chances_of_success").HasPrecision(18, 5);
        builder.Property(x => x.WizardEstimatedClosingDate).HasColumnName("wizard_estimated_closing_date");
        builder.Property(x => x.WizardContactName).HasColumnName("wizard_contact_name");
        builder.Property(x => x.WizardCampaignDescription).HasColumnName("wizard_campaign_description");
    }
}
