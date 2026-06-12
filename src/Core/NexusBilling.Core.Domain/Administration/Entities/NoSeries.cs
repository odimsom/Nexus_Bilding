using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class NoSeries : Entity
{
    private NoSeries() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool DefaultNos { get; set; }
    public bool ManualNos { get; set; }
    public bool DateOrder { get; set; }

    public static OperationResult<NoSeries, DomainError> Create(TenantIdentifier tenantId, string code, string description)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<NoSeries, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        if (string.IsNullOrWhiteSpace(code))
            return OperationResult<NoSeries, DomainError>.Fail(DomainError.Validation("no_series.code_required", "El código es obligatorio."));

        var entity = new NoSeries
        {
            Id = Guid.NewGuid(),
            TenantId = tenantId,
            Code = code.Trim().ToUpperInvariant(),
            Description = description.Trim(),
            DefaultNos = true,
            ManualNos = false,
            DateOrder = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        return OperationResult<NoSeries, DomainError>.Ok(entity);
    }
}
