using MediatR;
using NexusBilling.Core.Domain.Common;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Commands.UpsertVendor;

public record UpsertVendorCommand(
    Guid TenantId,
    string? ExistingNo,
    string? No,
    string Name,
    string Address,
    string City,
    string Contact) : IRequest<UpsertVendorResult>;

public record UpsertVendorResult(bool Created, string No);
