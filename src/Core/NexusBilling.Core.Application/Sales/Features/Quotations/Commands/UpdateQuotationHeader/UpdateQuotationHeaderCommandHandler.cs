using MediatR;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Commands.UpdateQuotationHeader;

public sealed class UpdateQuotationHeaderCommandHandler(
    ISalesHeaderRepository headerRepo,
    IUnitOfWork uow)
    : IRequestHandler<UpdateQuotationHeaderCommand, bool>
{
    public async Task<bool> Handle(UpdateQuotationHeaderCommand cmd, CancellationToken ct)
    {
        var quote = await headerRepo.GetByNoForTenantAsync(cmd.TenantId, cmd.No, ct)
            ?? throw new InvalidOperationException($"Cotización {cmd.No} no encontrada.");

        if (quote.DocumentType != "Quote")
            throw new InvalidOperationException("El documento no es una cotización.");

        if (quote.Status != "Open")
            throw new InvalidOperationException("Solo se pueden editar cotizaciones abiertas.");

        static DateTime? UtcN(DateTime? d) => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null;

        quote.ValidUntilDate = UtcN(cmd.ValidUntilDate);
        quote.DueDate = UtcN(cmd.ValidUntilDate);
        quote.QuotedBy = cmd.QuotedBy ?? string.Empty;
        quote.Observations = cmd.Observations ?? string.Empty;
        quote.CurrencyCode = cmd.CurrencyCode;
        quote.PaymentTermsCode = cmd.PaymentTermsCode;
        quote.PaymentMethodCode = cmd.PaymentMethodCode;
        quote.ExternalDocumentNo = cmd.ExternalDocumentNo ?? string.Empty;

        await headerRepo.UpdateAsync(quote, ct);
        await uow.SaveChangesAsync(ct);
        return true;
    }
}
