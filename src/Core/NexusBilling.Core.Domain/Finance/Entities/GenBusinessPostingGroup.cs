using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GenBusinessPostingGroup : Entity
{
    private GenBusinessPostingGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string DefVatBusPostingGroup { get; private set; }
    public bool AutoInsertDefault { get; private set; }

    public static OperationResult<GenBusinessPostingGroup, DomainError> Create(
        TenantIdentifier tenantId,
        string code,
        string description,
        string defVatBusPostingGroup,
        bool autoInsertDefault)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GenBusinessPostingGroup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(code))
            return OperationResult<GenBusinessPostingGroup, DomainError>.Fail(DomainError.Validation("finance.code_required", "El campo code es obligatorio."));
        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<GenBusinessPostingGroup, DomainError>.Fail(DomainError.Validation("finance.description_required", "El campo description es obligatorio."));
        if (string.IsNullOrWhiteSpace(defVatBusPostingGroup))
            return OperationResult<GenBusinessPostingGroup, DomainError>.Fail(DomainError.Validation("finance.def_vat_bus_posting_group_required", "El campo def_vat_bus_posting_group es obligatorio."));

        var entity = new GenBusinessPostingGroup()
        {
            TenantId = tenantId,
            Code = code.Trim(),
            Description = description.Trim(),
            DefVatBusPostingGroup = defVatBusPostingGroup.Trim(),
            AutoInsertDefault = autoInsertDefault,
        };

        return OperationResult<GenBusinessPostingGroup, DomainError>.Ok(entity);
    }
}
