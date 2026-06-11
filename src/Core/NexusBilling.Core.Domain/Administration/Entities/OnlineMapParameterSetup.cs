using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class OnlineMapParameterSetup : Entity
{
    private OnlineMapParameterSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string MapService { get; private set; }
    public string DirectionsService { get; private set; }
    public string Comment { get; private set; }
    public bool UrlEncodeNonAsciiChars { get; private set; }
    public string MilesKilometersOptionList { get; private set; }
    public string QuickestShortestOptionList { get; private set; }
    public string DirectionsFromLocationServ { get; private set; }

    public static OperationResult<OnlineMapParameterSetup, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<OnlineMapParameterSetup, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new OnlineMapParameterSetup()
        {
            TenantId = tenantId
        };
        return OperationResult<OnlineMapParameterSetup, DomainError>.Ok(entity);
    }
}
