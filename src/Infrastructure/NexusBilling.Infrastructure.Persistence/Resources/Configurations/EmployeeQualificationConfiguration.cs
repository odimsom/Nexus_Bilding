using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class EmployeeQualificationConfiguration : IEntityTypeConfiguration<EmployeeQualification>
{
    public void Configure(EntityTypeBuilder<EmployeeQualification> builder)
    {
        builder.ToTable("employee_qualification", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EmployeeNo).HasColumnName("employee_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.QualificationCode).HasColumnName("qualification_code");
        builder.Property(x => x.FromDate).HasColumnName("from_date");
        builder.Property(x => x.ToDate).HasColumnName("to_date");
        builder.Property(x => x.Type).HasColumnName("type");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.InstitutionCompany).HasColumnName("institution_company");
        builder.Property(x => x.Cost).HasColumnName("cost").HasPrecision(18, 5);
        builder.Property(x => x.CourseGrade).HasColumnName("course_grade");
        builder.Property(x => x.EmployeeStatus).HasColumnName("employee_status");
        builder.Property(x => x.ExpirationDate).HasColumnName("expiration_date");
    }
}
