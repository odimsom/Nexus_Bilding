using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GLBudgetName : Entity
{
    private GLBudgetName() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Blocked { get; private set; }
    public string? BudgetDimension1Code { get; private set; }
    public string? BudgetDimension2Code { get; private set; }
    public string? BudgetDimension3Code { get; private set; }
    public string? BudgetDimension4Code { get; private set; }

    public static OperationResult<GLBudgetName, DomainError> Create(
        TenantIdentifier tenantId,
        string name,
        string description,
        bool blocked,
        string? budgetDimension1Code,
        string? budgetDimension2Code,
        string? budgetDimension3Code,
        string? budgetDimension4Code)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GLBudgetName, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(name))
            return OperationResult<GLBudgetName, DomainError>.Fail(DomainError.Validation("finance.name_required", "El campo name es obligatorio."));
        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<GLBudgetName, DomainError>.Fail(DomainError.Validation("finance.description_required", "El campo description es obligatorio."));

        var entity = new GLBudgetName()
        {
            TenantId = tenantId,
            Name = name.Trim(),
            Description = description.Trim(),
            Blocked = blocked,
            BudgetDimension1Code = budgetDimension1Code,
            BudgetDimension2Code = budgetDimension2Code,
            BudgetDimension3Code = budgetDimension3Code,
            BudgetDimension4Code = budgetDimension4Code,
        };

        return OperationResult<GLBudgetName, DomainError>.Ok(entity);
    }
}
