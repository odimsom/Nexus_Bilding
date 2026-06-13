using MediatR;
using NexusBilling.Core.Application.Inventory.DTOs;

namespace NexusBilling.Core.Application.Inventory.Features.Items.Queries.GetItemByNo;

public record GetItemByNoQuery(Guid TenantId, string No) : IRequest<ItemDto?>;
