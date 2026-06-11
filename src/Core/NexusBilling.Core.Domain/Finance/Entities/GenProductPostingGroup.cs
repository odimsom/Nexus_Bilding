using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GenProductPostingGroup : Entity
{
    private GenProductPostingGroup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string DefVatProdPostingGroup { get; private set; }
    public bool AutoInsertDefault { get; private set; }

    public static OperationResult<GenProductPostingGroup, DomainError> Create(
        TenantIdentifier tenantId,
        string code,
        string description,
        string defVatProdPostingGroup,
        bool autoInsertDefault)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GenProductPostingGroup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(code))
            return OperationResult<GenProductPostingGroup, DomainError>.Fail(DomainError.Validation("finance.code_required", "El campo code es obligatorio."));
        if (string.IsNullOrWhiteSpace(description))
            return OperationResult<GenProductPostingGroup, DomainError>.Fail(DomainError.Validation("finance.description_required", "El campo description es obligatorio."));
        if (string.IsNullOrWhiteSpace(defVatProdPostingGroup))
            return OperationResult<GenProductPostingGroup, DomainError>.Fail(DomainError.Validation("finance.def_vat_prod_posting_group_required", "El campo def_vat_prod_posting_group es obligatorio."));

        var entity = new GenProductPostingGroup()
        {
            TenantId = tenantId,
            Code = code.Trim(),
            Description = description.Trim(),
            DefVatProdPostingGroup = defVatProdPostingGroup.Trim(),
            AutoInsertDefault = autoInsertDefault,
        };

        return OperationResult<GenProductPostingGroup, DomainError>.Ok(entity);
    }
}
