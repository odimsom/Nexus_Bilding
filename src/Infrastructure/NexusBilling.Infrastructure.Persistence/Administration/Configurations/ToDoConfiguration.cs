using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ToTable("to_do", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.TeamCode).HasColumnName("team_code");
        builder.Property(x => x.SalespersonCode).HasColumnName("salesperson_code");
        builder.Property(x => x.CampaignNo).HasColumnName("campaign_no");
        builder.Property(x => x.ContactNo).HasColumnName("contact_no");
        builder.Property(x => x.OpportunityNo).HasColumnName("opportunity_no");
        builder.Property(x => x.SegmentNo).HasColumnName("segment_no");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.Priority).HasColumnName("priority");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Closed).HasColumnName("closed");
        builder.Property(x => x.DateClosed).HasColumnName("date_closed");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.Canceled).HasColumnName("canceled");
        builder.Property(x => x.ContactCompanyNo).HasColumnName("contact_company_no");
        builder.Property(x => x.Recurring).HasColumnName("recurring");
        builder.Property(x => x.RecurringDateInterval).HasColumnName("recurring_date_interval");
        builder.Property(x => x.CalcDueDateFrom).HasColumnName("calc_due_date_from");
        builder.Property(x => x.StartTime).HasColumnName("start_time");
        builder.Property(x => x.Duration).HasColumnName("duration");
        builder.Property(x => x.OpportunityEntryNo).HasColumnName("opportunity_entry_no");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.LastTimeModified).HasColumnName("last_time_modified");
        builder.Property(x => x.AllDayEvent).HasColumnName("all_day_event");
        builder.Property(x => x.Location).HasColumnName("location");
        builder.Property(x => x.OrganizerToDoNo).HasColumnName("organizer_to_do_no");
        builder.Property(x => x.InteractionTemplateCode).HasColumnName("interaction_template_code");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.AttachmentNo).HasColumnName("attachment_no");
        builder.Property(x => x.Subject).HasColumnName("subject");
        builder.Property(x => x.UnitCostLcy).HasColumnName("unit_cost_lcy").HasPrecision(18, 5);
        builder.Property(x => x.UnitDurationMin).HasColumnName("unit_duration_min").HasPrecision(18, 5);
        builder.Property(x => x.SystemToDoType).HasColumnName("system_to_do_type");
        builder.Property(x => x.CompletedBy).HasColumnName("completed_by");
        builder.Property(x => x.EndingDate).HasColumnName("ending_date");
        builder.Property(x => x.EndingTime).HasColumnName("ending_time");
        builder.Property(x => x.WizardStep).HasColumnName("wizard_step");
        builder.Property(x => x.TeamToDo).HasColumnName("team_to_do");
        builder.Property(x => x.SendOnFinish).HasColumnName("send_on_finish");
        builder.Property(x => x.SegmentDescription).HasColumnName("segment_description");
        builder.Property(x => x.TeamMeetingOrganizer).HasColumnName("team_meeting_organizer");
        builder.Property(x => x.ActivityCode).HasColumnName("activity_code");
        builder.Property(x => x.WizardContactName).HasColumnName("wizard_contact_name");
        builder.Property(x => x.WizardCampaignDescription).HasColumnName("wizard_campaign_description");
        builder.Property(x => x.WizardOpportunityDescription).HasColumnName("wizard_opportunity_description");
    }
}
