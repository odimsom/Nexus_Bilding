using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Resources.Entities;

public class EmployeeAbsence : Entity
{
    private EmployeeAbsence() { }

    public TenantIdentifier TenantId { get; private set; }
    public string EmployeeNo { get; private set; }
    public int EntryNo { get; private set; }
    public DateTime? FromDate { get; private set; }
    public DateTime? ToDate { get; private set; }
    public string CauseOfAbsenceCode { get; private set; }
    public string Description { get; private set; }
    public decimal Quantity { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QuantityBase { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }

    public static OperationResult<EmployeeAbsence, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<EmployeeAbsence, DomainError>.Fail(DomainError.Validation("resources.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new EmployeeAbsence()
        {
            TenantId = tenantId
        };
        return OperationResult<EmployeeAbsence, DomainError>.Ok(entity);
    }
}
