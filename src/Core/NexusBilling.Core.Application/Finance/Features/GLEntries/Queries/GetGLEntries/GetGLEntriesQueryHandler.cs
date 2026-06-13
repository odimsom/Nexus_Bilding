using MediatR;
using NexusBilling.Core.Application.Finance.DTOs;
using NexusBilling.Core.Domain.Finance.Repositories;

namespace NexusBilling.Core.Application.Finance.Features.GLEntries.Queries.GetGLEntries;

public sealed class GetGLEntriesQueryHandler(
    IGLEntryRepository glEntryRepo,
    IGLAccountRepository glAccountRepo)
    : IRequestHandler<GetGLEntriesQuery, GetGLEntriesResult>
{
    public async Task<GetGLEntriesResult> Handle(GetGLEntriesQuery request, CancellationToken cancellationToken)
    {
        var (entries, total) = await glEntryRepo.ListAsync(
            request.TenantId, request.GLAccountNo, request.Page, request.PageSize, cancellationToken);

        // Normally we might join or include the GLAccount to get the Name, but let's fetch it for simplicity
        // in a real scenario, use Include in EF or Dapper for performance.
        var dtos = new List<GLEntryDto>();
        foreach (var entry in entries)
        {
            var acc = await glAccountRepo.GetByNoAsync(request.TenantId, entry.GLAccountNo, cancellationToken);
            dtos.Add(new GLEntryDto(
                entry.EntryNo,
                entry.GLAccountNo,
                acc?.Name ?? string.Empty,
                DateTime.TryParse(entry.PostingDate, out var d) ? d : DateTime.MinValue,
                entry.DocumentType.ToString(),
                entry.DocumentNo,
                entry.Description,
                entry.Amount,
                entry.SourceCode
            ));
        }

        var totalPages = (int)Math.Ceiling(total / (double)request.PageSize);

        return new GetGLEntriesResult(dtos, total, totalPages);
    }
}
