using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Commands.ConvertQuotationToOrder;

public sealed class ConvertQuotationToOrderCommandHandler(
    ISalesHeaderRepository headerRepo,
    ISalesLineRepository lineRepo,
    NoSeriesService noSeriesService,
    IUnitOfWork uow)
    : IRequestHandler<ConvertQuotationToOrderCommand, ConvertQuotationToOrderResult>
{
    public async Task<ConvertQuotationToOrderResult> Handle(ConvertQuotationToOrderCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);
        var quote = await headerRepo.GetByNoForTenantAsync(cmd.TenantId, cmd.QuotationNo, ct)
            ?? throw new InvalidOperationException($"Cotización {cmd.QuotationNo} no encontrada.");

        if (quote.DocumentType != "Quote")
            throw new InvalidOperationException("El documento no es una cotización.");

        if (quote.ValidUntilDate.HasValue && quote.ValidUntilDate.Value < DateTime.UtcNow)
            throw new InvalidOperationException("La cotización ha vencido y no puede convertirse en orden.");

        string orderNo = cmd.SeriesCode is not null
            ? await noSeriesService.GetNextNoAsync(cmd.TenantId, cmd.SeriesCode, ct)
            : throw new InvalidOperationException("SeriesCode es requerido.");

        var headerResult = SalesHeader.Create(
            tid, "Order", orderNo,
            quote.SellToCustomerNo, quote.SellToCustomerName, quote.BillToName,
            DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc));

        if (!headerResult.IsSuccess)
            throw new InvalidOperationException(headerResult.GetError()!.Message);

        var order = headerResult.GetValue()!;
        order.DueDate = quote.DueDate;
        order.CurrencyCode = quote.CurrencyCode;
        order.PaymentTermsCode = quote.PaymentTermsCode;
        order.PaymentMethodCode = quote.PaymentMethodCode;
        order.SalespersonCode = quote.SalespersonCode;
        order.ExternalDocumentNo = quote.ExternalDocumentNo;
        order.Observations = quote.Observations;
        order.Amount = quote.Amount;
        order.AmountIncludingVat = quote.AmountIncludingVat;
        order.Status = "Open";

        // Copy lines
        var quoteLines = await lineRepo.GetByDocumentNoAsync(0 /* Quote */, quote.No, ct);
        int lineNo = 10000;
        var newLines = new List<SalesLine>();
        foreach (var ql in quoteLines.OrderBy(l => l.LineNo))
        {
            var lResult = SalesLine.Create(tid, 1 /* Order */, orderNo, lineNo);
            if (!lResult.IsSuccess) continue;
            var nl = lResult.GetValue()!;
            nl.SellToCustomerNo = ql.SellToCustomerNo;
            nl.Type = ql.Type;
            nl.No = ql.No;
            nl.Description = ql.Description;
            nl.UnitOfMeasure = ql.UnitOfMeasure;
            nl.Quantity = ql.Quantity;
            nl.UnitPrice = ql.UnitPrice;
            nl.Vat = ql.Vat;
            nl.LineDiscount = ql.LineDiscount;
            nl.LineDiscountAmount = ql.LineDiscountAmount;
            nl.Amount = ql.Amount;
            nl.AmountIncludingVat = ql.AmountIncludingVat;
            nl.OutstandingQuantity = ql.Quantity;
            nl.OutstandingAmount = ql.Amount;
            nl.QtyToInvoice = ql.Quantity;
            nl.QtyToShip = ql.Quantity;
            nl.ServiceBillingType = ql.ServiceBillingType;
            nl.ServiceStartDate = ql.ServiceStartDate;
            nl.ServiceEndDate = ql.ServiceEndDate;
            nl.ServiceHours = ql.ServiceHours;
            nl.HourlyRate = ql.HourlyRate;
            nl.ResourceNo = ql.ResourceNo;
            newLines.Add(nl);
            lineNo += 10000;
        }

        // Mark quote as closed
        quote.Close();

        await headerRepo.AddAsync(order, ct);
        foreach (var nl in newLines)
            await lineRepo.AddAsync(nl, ct);

        await uow.SaveChangesAsync(ct);
        return new ConvertQuotationToOrderResult(orderNo);
    }
}
