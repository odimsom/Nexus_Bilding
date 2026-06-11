using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Administration.Entities;

public class WebhookSubscription : Entity
{
    private WebhookSubscription() { }

    public TenantIdentifier TenantId { get; private set; }
    public string SubscriptionId { get; private set; }
    public string Endpoint { get; private set; }
    public string ClientState { get; private set; }
    public string CreatedBy { get; private set; }
    public Guid RunNotificationAs { get; private set; }
    public string CompanyName { get; private set; }

    public static OperationResult<WebhookSubscription, DomainError> Create(TenantIdentifier tenantId)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<WebhookSubscription, DomainError>.Fail(DomainError.Validation("administration.tenant_required", "El TenantIdentifier es obligatorio."));

        var entity = new WebhookSubscription()
        {
            TenantId = tenantId
        };
        return OperationResult<WebhookSubscription, DomainError>.Ok(entity);
    }
}
