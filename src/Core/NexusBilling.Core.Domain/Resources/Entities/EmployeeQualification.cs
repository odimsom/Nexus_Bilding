using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class EmployeeQualification : Entity
{
    private EmployeeQualification() { }

    public TenantIdentifier TenantId { get; private set; }
    public string EmployeeNo { get; private set; }
    public int LineNo { get; private set; }
    public string QualificationCode { get; private set; }
    public DateTime? FromDate { get; private set; }
    public DateTime? ToDate { get; private set; }
    public short Type { get; private set; }
    public string Description { get; private set; }
    public string InstitutionCompany { get; private set; }
    public decimal Cost { get; private set; }
    public string CourseGrade { get; private set; }
    public short EmployeeStatus { get; private set; }
    public DateTime? ExpirationDate { get; private set; }

    public static OperationResult<EmployeeQualification, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<EmployeeQualification, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new EmployeeQualification()
        {
            TenantId = tenantId
        };
        return OperationResult<EmployeeQualification, DomainError>.Ok(entity);
    }
}
