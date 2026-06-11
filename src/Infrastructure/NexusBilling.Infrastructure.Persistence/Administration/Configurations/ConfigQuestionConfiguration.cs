using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigQuestionConfiguration : IEntityTypeConfiguration<ConfigQuestion>
{
    public void Configure(EntityTypeBuilder<ConfigQuestion> builder)
    {
        builder.ToTable("config_question", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.QuestionnaireCode).HasColumnName("questionnaire_code");
        builder.Property(x => x.QuestionAreaCode).HasColumnName("question_area_code");
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Question).HasColumnName("question");
        builder.Property(x => x.AnswerOption).HasColumnName("answer_option");
        builder.Property(x => x.Answer).HasColumnName("answer");
        builder.Property(x => x.TableId).HasColumnName("table_id");
        builder.Property(x => x.FieldId).HasColumnName("field_id");
        builder.Property(x => x.Reference).HasColumnName("reference");
        builder.Property(x => x.QuestionOrigin).HasColumnName("question_origin");
    }
}
