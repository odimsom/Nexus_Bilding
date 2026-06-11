using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ConfigQuestionAreaConfiguration : IEntityTypeConfiguration<ConfigQuestionArea>
{
    public void Configure(EntityTypeBuilder<ConfigQuestionArea> builder)
    {
        builder.ToTable("config_question_area", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.QuestionnaireCode).HasColumnName("questionnaire_code");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.TableId).HasColumnName("table_id");
    }
}
