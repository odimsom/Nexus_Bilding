using MediatR;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Commands.DuplicateQuotation;

public record DuplicateQuotationCommand(
    Guid TenantId,
    string SourceNo,
    DateTime NewPostingDate,
    DateTime? NewValidUntilDate,
    string? SeriesCode) : IRequest<DuplicateQuotationResult>;

public record DuplicateQuotationResult(string No);
