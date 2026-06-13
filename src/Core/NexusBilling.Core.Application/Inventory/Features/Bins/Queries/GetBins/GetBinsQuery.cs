using MediatR;
using System.Collections.Generic;

namespace NexusBilling.Core.Application.Inventory.Features.Bins.Queries.GetBins;

public record BinDto(string LocationCode, string Code, string Description, string ZoneCode);

public record GetBinsQuery(Guid TenantId, string LocationCode) : IRequest<List<BinDto>>;
