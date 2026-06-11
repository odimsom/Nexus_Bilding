using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class InteractionLogEntryConfiguration : IEntityTypeConfiguration<InteractionLogEntry>
{
    public void Configure(EntityTypeBuilder<InteractionLogEntry> builder)
    {
        builder.ToTable("interaction_log_entry", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EntryNo).HasColumnName("entry_no");
        builder.Property(x => x.ContactNo).HasColumnName("contact_no");
        builder.Property(x => x.ContactCompanyNo).HasColumnName("contact_company_no");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.InformationFlow).HasColumnName("information_flow");
        builder.Property(x => x.InitiatedBy).HasColumnName("initiated_by");
        builder.Property(x => x.AttachmentNo).HasColumnName("attachment_no");
        builder.Property(x => x.CostLcy).HasColumnName("cost_lcy").HasPrecision(18, 5);
        builder.Property(x => x.DurationMin).HasColumnName("duration_min").HasPrecision(18, 5);
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.InteractionGroupCode).HasColumnName("interaction_group_code");
        builder.Property(x => x.InteractionTemplateCode).HasColumnName("interaction_template_code");
        builder.Property(x => x.CampaignNo).HasColumnName("campaign_no");
        builder.Property(x => x.CampaignEntryNo).HasColumnName("campaign_entry_no");
        builder.Property(x => x.CampaignResponse).HasColumnName("campaign_response");
        builder.Property(x => x.CampaignTarget).HasColumnName("campaign_target");
        builder.Property(x => x.SegmentNo).HasColumnName("segment_no");
        builder.Property(x => x.Evaluation).HasColumnName("evaluation");
        builder.Property(x => x.TimeOfInteraction).HasColumnName("time_of_interaction");
        builder.Property(x => x.AttemptFailed).HasColumnName("attempt_failed");
        builder.Property(x => x.ToDoNo).HasColumnName("to_do_no");
        builder.Property(x => x.SalespersonCode).HasColumnName("salesperson_code");
        builder.Property(x => x.DeliveryStatus).HasColumnName("delivery_status");
        builder.Property(x => x.Canceled).HasColumnName("canceled");
        builder.Property(x => x.CorrespondenceType).HasColumnName("correspondence_type");
        builder.Property(x => x.ContactAltAddressCode).HasColumnName("contact_alt_address_code");
        builder.Property(x => x.LoggedSegmentEntryNo).HasColumnName("logged_segment_entry_no");
        builder.Property(x => x.DocumentType).HasColumnName("document_type");
        builder.Property(x => x.DocumentNo).HasColumnName("document_no");
        builder.Property(x => x.VersionNo).HasColumnName("version_no");
        builder.Property(x => x.DocNoOccurrence).HasColumnName("doc_no_occurrence");
        builder.Property(x => x.ContactVia).HasColumnName("contact_via");
        builder.Property(x => x.SendWordDocsAsAttmt).HasColumnName("send_word_docs_as_attmt");
        builder.Property(x => x.InteractionLanguageCode).HasColumnName("interaction_language_code");
        builder.Property(x => x.EMailLogged).HasColumnName("e_mail_logged");
        builder.Property(x => x.Subject).HasColumnName("subject");
        builder.Property(x => x.OpportunityNo).HasColumnName("opportunity_no");
        builder.Property(x => x.Postponed).HasColumnName("postponed");
    }
}
