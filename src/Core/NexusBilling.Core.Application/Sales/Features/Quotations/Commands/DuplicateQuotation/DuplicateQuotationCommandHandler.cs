using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Commands.DuplicateQuotation;

public sealed class DuplicateQuotationCommandHandler(
    ISalesHeaderRepository headerRepo,
    ISalesLineRepository lineRepo,
    NoSeriesService noSeriesService,
    IUnitOfWork uow)
    : IRequestHandler<DuplicateQuotationCommand, DuplicateQuotationResult>
{
    public async Task<DuplicateQuotationResult> Handle(DuplicateQuotationCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);
        var source = await headerRepo.GetByNoForTenantAsync(cmd.TenantId, cmd.SourceNo, ct)
            ?? throw new InvalidOperationException($"Cotización {cmd.SourceNo} no encontrada.");

        string newNo = cmd.SeriesCode is not null
            ? await noSeriesService.GetNextNoAsync(cmd.TenantId, cmd.SeriesCode, ct)
            : throw new InvalidOperationException("SeriesCode es requerido.");

        static DateTime Utc(DateTime d) => DateTime.SpecifyKind(d, DateTimeKind.Utc);
        static DateTime? UtcN(DateTime? d) => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null;

        var headerResult = SalesHeader.Create(
            tid, "Quote", newNo,
            source.SellToCustomerNo, source.SellToCustomerName, source.BillToName,
            Utc(cmd.NewPostingDate));

        if (!headerResult.IsSuccess)
            throw new InvalidOperationException(headerResult.GetError()!.Message);

        var newHeader = headerResult.GetValue()!;
        newHeader.DueDate = UtcN(cmd.NewValidUntilDate);
        newHeader.ValidUntilDate = UtcN(cmd.NewValidUntilDate);
        newHeader.QuotedBy = source.QuotedBy;
        newHeader.Observations = source.Observations;
        newHeader.CurrencyCode = source.CurrencyCode;
        newHeader.PaymentTermsCode = source.PaymentTermsCode;
        newHeader.PaymentMethodCode = source.PaymentMethodCode;
        newHeader.ExternalDocumentNo = source.ExternalDocumentNo;
        newHeader.Amount = source.Amount;
        newHeader.AmountIncludingVat = source.AmountIncludingVat;
        newHeader.Status = "Open";

        var sourceLines = await lineRepo.GetByDocumentNoAsync(0 /* Quote */, source.No, ct);
        int lineNo = 10000;
        var newLines = new List<SalesLine>();
        foreach (var sl in sourceLines.OrderBy(l => l.LineNo))
        {
            var lResult = SalesLine.Create(tid, 0 /* Quote */, newNo, lineNo);
            if (!lResult.IsSuccess) continue;
            var nl = lResult.GetValue()!;
            nl.SellToCustomerNo = sl.SellToCustomerNo;
            nl.Type = sl.Type;
            nl.No = sl.No;
            nl.Description = sl.Description;
            nl.UnitOfMeasure = sl.UnitOfMeasure;
            nl.Quantity = sl.Quantity;
            nl.UnitPrice = sl.UnitPrice;
            nl.Vat = sl.Vat;
            nl.LineDiscount = sl.LineDiscount;
            nl.LineDiscountAmount = sl.LineDiscountAmount;
            nl.Amount = sl.Amount;
            nl.AmountIncludingVat = sl.AmountIncludingVat;
            nl.OutstandingQuantity = sl.Quantity;
            nl.ServiceBillingType = sl.ServiceBillingType;
            nl.ServiceStartDate = sl.ServiceStartDate;
            nl.ServiceEndDate = sl.ServiceEndDate;
            nl.ServiceHours = sl.ServiceHours;
            nl.HourlyRate = sl.HourlyRate;
            nl.ResourceNo = sl.ResourceNo;
            newLines.Add(nl);
            lineNo += 10000;
        }

        await headerRepo.AddAsync(newHeader, ct);
        foreach (var nl in newLines)
            await lineRepo.AddAsync(nl, ct);

        await uow.SaveChangesAsync(ct);
        return new DuplicateQuotationResult(newNo);
    }
}
