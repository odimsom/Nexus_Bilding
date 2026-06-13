using MediatR;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Commands.CreateVendor;

public record CreateVendorCommand(
    Guid TenantId,
    string Name,
    string Address,
    string City,
    string Contact) : IRequest<string>;
