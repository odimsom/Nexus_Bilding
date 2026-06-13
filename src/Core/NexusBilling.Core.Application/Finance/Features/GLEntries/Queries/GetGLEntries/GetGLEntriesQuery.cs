using MediatR;
using NexusBilling.Core.Application.Finance.DTOs;

namespace NexusBilling.Core.Application.Finance.Features.GLEntries.Queries.GetGLEntries;

public record GetGLEntriesQuery(
    Guid TenantId,
    string? GLAccountNo,
    int Page,
    int PageSize
) : IRequest<GetGLEntriesResult>;

public record GetGLEntriesResult(
    IReadOnlyList<GLEntryDto> Entries,
    int TotalItems,
    int TotalPages
);
