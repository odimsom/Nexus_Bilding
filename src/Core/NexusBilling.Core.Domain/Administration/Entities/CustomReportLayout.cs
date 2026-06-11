using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class CustomReportLayout : Entity
{
    private CustomReportLayout() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public int ReportId { get; private set; }
    public string CompanyName { get; private set; }
    public short Type { get; private set; }
    public byte[]? Layout { get; private set; }
    public DateTime? LastModified { get; private set; }
    public string LastModifiedByUser { get; private set; }
    public string FileExtension { get; private set; }
    public string Description { get; private set; }
    public byte[]? CustomXmlPart { get; private set; }
    public Guid AppId { get; private set; }
    public bool BuiltIn { get; private set; }

    public static OperationResult<CustomReportLayout, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<CustomReportLayout, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new CustomReportLayout()
        {
            TenantId = tenantId
        };
        return OperationResult<CustomReportLayout, DomainError>.Ok(entity);
    }
}
