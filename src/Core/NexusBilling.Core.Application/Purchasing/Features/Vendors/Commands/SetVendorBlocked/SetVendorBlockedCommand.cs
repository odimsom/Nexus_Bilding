using MediatR;
using NexusBilling.Core.Domain.Common;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Commands.SetVendorBlocked;

public record SetVendorBlockedCommand(Guid TenantId, string No, bool Blocked) : IRequest<bool>;
