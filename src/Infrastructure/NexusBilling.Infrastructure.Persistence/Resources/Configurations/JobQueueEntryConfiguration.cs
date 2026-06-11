using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class JobQueueEntryConfiguration : IEntityTypeConfiguration<JobQueueEntry>
{
    public void Configure(EntityTypeBuilder<JobQueueEntry> builder)
    {
        builder.ToTable("job_queue_entry", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Xml).HasColumnName("xml");
        builder.Property(x => x.LastReadyState).HasColumnName("last_ready_state");
        builder.Property(x => x.ExpirationDateTime).HasColumnName("expiration_date_time");
        builder.Property(x => x.EarliestStartDateTime).HasColumnName("earliest_start_date_time");
        builder.Property(x => x.ObjectTypeToRun).HasColumnName("object_type_to_run");
        builder.Property(x => x.ObjectIdToRun).HasColumnName("object_id_to_run");
        builder.Property(x => x.ReportOutputType).HasColumnName("report_output_type");
        builder.Property(x => x.MaximumNoOfAttemptsToRun).HasColumnName("maximum_no_of_attempts_to_run");
        builder.Property(x => x.NoOfAttemptsToRun).HasColumnName("no_of_attempts_to_run");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.Priority).HasColumnName("priority");
        builder.Property(x => x.RecordIdToProcess).HasColumnName("record_id_to_process");
        builder.Property(x => x.ParameterString).HasColumnName("parameter_string");
        builder.Property(x => x.RecurringJob).HasColumnName("recurring_job");
        builder.Property(x => x.NoOfMinutesBetweenRuns).HasColumnName("no_of_minutes_between_runs");
        builder.Property(x => x.RunOnMondays).HasColumnName("run_on_mondays");
        builder.Property(x => x.RunOnTuesdays).HasColumnName("run_on_tuesdays");
        builder.Property(x => x.RunOnWednesdays).HasColumnName("run_on_wednesdays");
        builder.Property(x => x.RunOnThursdays).HasColumnName("run_on_thursdays");
        builder.Property(x => x.RunOnFridays).HasColumnName("run_on_fridays");
        builder.Property(x => x.RunOnSaturdays).HasColumnName("run_on_saturdays");
        builder.Property(x => x.RunOnSundays).HasColumnName("run_on_sundays");
        builder.Property(x => x.StartingTime).HasColumnName("starting_time");
        builder.Property(x => x.EndingTime).HasColumnName("ending_time");
        builder.Property(x => x.ReferenceStartingTime).HasColumnName("reference_starting_time");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.RunInUserSession).HasColumnName("run_in_user_session");
        builder.Property(x => x.UserSessionId).HasColumnName("user_session_id");
        builder.Property(x => x.JobQueueCategoryCode).HasColumnName("job_queue_category_code");
        builder.Property(x => x.ErrorMessage).HasColumnName("error_message");
        builder.Property(x => x.ErrorMessage2).HasColumnName("error_message_2");
        builder.Property(x => x.ErrorMessage3).HasColumnName("error_message_3");
        builder.Property(x => x.ErrorMessage4).HasColumnName("error_message_4");
        builder.Property(x => x.UserServiceInstanceId).HasColumnName("user_service_instance_id");
        builder.Property(x => x.UserSessionStarted).HasColumnName("user_session_started");
        builder.Property(x => x.TimeoutSec).HasColumnName("timeout_sec");
        builder.Property(x => x.NotifyOnSuccess).HasColumnName("notify_on_success");
        builder.Property(x => x.UserLanguageId).HasColumnName("user_language_id");
        builder.Property(x => x.PrinterName).HasColumnName("printer_name");
        builder.Property(x => x.ReportRequestPageOptions).HasColumnName("report_request_page_options");
        builder.Property(x => x.RerunDelaySec).HasColumnName("rerun_delay_sec");
        builder.Property(x => x.SystemTaskId).HasColumnName("system_task_id");
    }
}
