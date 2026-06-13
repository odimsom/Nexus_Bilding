using MediatR;
using NexusBilling.Core.Application.Purchasing.Features.Vendors.Queries.GetVendors;

namespace NexusBilling.Core.Application.Purchasing.Features.Vendors.Queries.GetVendorByNo;

public record GetVendorByNoQuery(Guid TenantId, string No) : IRequest<VendorDto?>;
