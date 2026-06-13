using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.SalesOrders.Commands.CreateSalesOrder;

public sealed class CreateSalesOrderCommandHandler(
    ISalesHeaderRepository headerRepo,
    ISalesLineRepository lineRepo,
    NoSeriesService noSeriesService,
    IUnitOfWork uow)
    : IRequestHandler<CreateSalesOrderCommand, CreateSalesOrderResult>
{
    private const decimal VatRate = 0.18m; // ITBIS dominicano

    public async Task<CreateSalesOrderResult> Handle(CreateSalesOrderCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        // Resolve document number
        string docNo = cmd.SeriesCode is not null
            ? await noSeriesService.GetNextNoAsync(cmd.TenantId, cmd.SeriesCode, ct)
            : cmd.ManualNo ?? throw new InvalidOperationException("Se requiere SeriesCode o ManualNo.");

        static DateTime Utc(DateTime d) => DateTime.SpecifyKind(d, DateTimeKind.Utc);
        static DateTime? UtcN(DateTime? d) => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null;

        var headerResult = SalesHeader.Create(
            tid, cmd.DocumentType, docNo,
            cmd.SellToCustomerNo, cmd.SellToCustomerName, cmd.SellToCustomerName,
            Utc(cmd.PostingDate));

        if (!headerResult.IsSuccess)
            throw new InvalidOperationException(headerResult.GetError()!.Message);

        var header = headerResult.GetValue()!;
        header.DueDate = UtcN(cmd.DueDate);
        header.CurrencyCode = cmd.CurrencyCode ?? string.Empty;
        header.PaymentTermsCode = cmd.PaymentTermsCode ?? string.Empty;
        header.PaymentMethodCode = cmd.PaymentMethodCode ?? string.Empty;
        header.SalespersonCode = cmd.SalespersonCode ?? string.Empty;
        header.ExternalDocumentNo = cmd.ExternalDocumentNo ?? string.Empty;
        header.Status = "Open";

        // Build lines and compute totals
        decimal totalAmount = 0m;
        decimal totalAmountVat = 0m;
        int lineNo = 10000;

        var lineEntities = new List<SalesLine>();
        foreach (var l in cmd.Lines)
        {
            var lineResult = SalesLine.Create(tid, 1 /* Order */, docNo, lineNo);
            if (!lineResult.IsSuccess) continue;

            var line = lineResult.GetValue()!;
            line.SellToCustomerNo = cmd.SellToCustomerNo;
            line.Type = l.LineType switch { "G/L Account" => 1, "Service" => 3, _ => 2 };
            line.No = l.ItemNo ?? string.Empty;
            line.Description = l.Description;
            line.UnitOfMeasure = l.UnitOfMeasure;
            line.Quantity = l.Quantity;
            line.UnitPrice = l.UnitPrice;
            line.Vat = VatRate * 100; // stored as percentage
            line.LineDiscount = l.LineDiscountPct;

            decimal lineAmt = l.Quantity * l.UnitPrice;
            if (l.LineDiscountPct > 0)
            {
                decimal discAmt = lineAmt * (l.LineDiscountPct / 100m);
                line.LineDiscountAmount = Math.Round(discAmt, 2);
                lineAmt -= discAmt;
            }
            line.Amount = Math.Round(lineAmt, 2);
            line.AmountIncludingVat = Math.Round(lineAmt * (1 + VatRate), 2);
            line.OutstandingQuantity = l.Quantity;
            line.QtyToInvoice = l.Quantity;
            line.QtyToShip = l.Quantity;
            line.OutstandingAmount = line.Amount;

            totalAmount += line.Amount;
            totalAmountVat += line.AmountIncludingVat;
            lineEntities.Add(line);
            lineNo += 10000;
        }

        header.Amount = Math.Round(totalAmount, 2);
        header.AmountIncludingVat = Math.Round(totalAmountVat, 2);

        await headerRepo.AddAsync(header, ct);
        foreach (var line in lineEntities)
            await lineRepo.AddAsync(line, ct);

        await uow.SaveChangesAsync(ct);
        return new CreateSalesOrderResult(docNo);
    }
}
