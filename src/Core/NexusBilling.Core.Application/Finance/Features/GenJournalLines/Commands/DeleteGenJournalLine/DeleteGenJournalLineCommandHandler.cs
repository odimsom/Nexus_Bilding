using MediatR;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Finance.Repositories;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;

namespace NexusBilling.Core.Application.Finance.Features.GenJournalLines.Commands.DeleteGenJournalLine;

internal sealed class DeleteGenJournalLineCommandHandler(
    IGenJournalLineRepository repo,
    IUnitOfWork uow)
    : IRequestHandler<DeleteGenJournalLineCommand, OperationResult<MediatR.Unit, DomainError>>
{
    public async Task<OperationResult<MediatR.Unit, DomainError>> Handle(DeleteGenJournalLineCommand request, CancellationToken cancellationToken)
    {
        var line = await repo.GetByIdAsync(request.Id);
        if (line == null)
            return OperationResult<MediatR.Unit, DomainError>.Fail(DomainError.NotFound("finance.line_not_found", "Línea de diario no encontrada."));

        await repo.DeleteAsync(line);
        await uow.SaveChangesAsync(cancellationToken);

        return OperationResult<MediatR.Unit, DomainError>.Ok(MediatR.Unit.Value);
    }
}
