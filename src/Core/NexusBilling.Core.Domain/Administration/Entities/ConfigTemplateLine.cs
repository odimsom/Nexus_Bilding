using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class ConfigTemplateLine : Entity
{
    private ConfigTemplateLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public string DataTemplateCode { get; private set; }
    public int LineNo { get; private set; }
    public short Type { get; private set; }
    public int FieldId { get; private set; }
    public string FieldName { get; private set; }
    public int TableId { get; private set; }
    public string TemplateCode { get; private set; }
    public bool Mandatory { get; private set; }
    public string Reference { get; private set; }
    public string DefaultValue { get; private set; }
    public bool SkipRelationCheck { get; private set; }
    public int LanguageId { get; private set; }

    public static OperationResult<ConfigTemplateLine, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<ConfigTemplateLine, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new ConfigTemplateLine()
        {
            TenantId = tenantId
        };
        return OperationResult<ConfigTemplateLine, DomainError>.Ok(entity);
    }
}
