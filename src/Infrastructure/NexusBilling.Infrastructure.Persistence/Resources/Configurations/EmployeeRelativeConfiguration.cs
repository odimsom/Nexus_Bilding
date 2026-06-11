using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Resources.Entities;

namespace NexusBilling.Infrastructure.Persistence.Resources.Configurations;

public class EmployeeRelativeConfiguration : IEntityTypeConfiguration<EmployeeRelative>
{
    public void Configure(EntityTypeBuilder<EmployeeRelative> builder)
    {
        builder.ToTable("employee_relative", "resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.EmployeeNo).HasColumnName("employee_no");
        builder.Property(x => x.LineNo).HasColumnName("line_no");
        builder.Property(x => x.RelativeCode).HasColumnName("relative_code");
        builder.Property(x => x.FirstName).HasColumnName("first_name");
        builder.Property(x => x.MiddleName).HasColumnName("middle_name");
        builder.Property(x => x.LastName).HasColumnName("last_name");
        builder.Property(x => x.BirthDate).HasColumnName("birth_date");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
        builder.Property(x => x.RelativeSEmployeeNo).HasColumnName("relative_s_employee_no");
    }
}
