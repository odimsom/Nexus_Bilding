using MediatR;
using NexusBilling.Core.Application.Administration.Services;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Sales.Entities;
using NexusBilling.Core.Domain.Sales.Repositories;

namespace NexusBilling.Core.Application.Sales.Features.Quotations.Commands.CreateQuotation;

public sealed class CreateQuotationCommandHandler(
    ISalesHeaderRepository headerRepo,
    ISalesLineRepository lineRepo,
    NoSeriesService noSeriesService,
    IUnitOfWork uow)
    : IRequestHandler<CreateQuotationCommand, CreateQuotationResult>
{
    public async Task<CreateQuotationResult> Handle(CreateQuotationCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        string docNo = cmd.SeriesCode is not null
            ? await noSeriesService.GetNextNoAsync(cmd.TenantId, cmd.SeriesCode, ct)
            : throw new InvalidOperationException("SeriesCode es requerido para cotizaciones.");

        static DateTime Utc(DateTime d) => DateTime.SpecifyKind(d, DateTimeKind.Utc);
        static DateTime? UtcN(DateTime? d) => d.HasValue ? DateTime.SpecifyKind(d.Value, DateTimeKind.Utc) : null;

        var headerResult = SalesHeader.Create(
            tid, "Quote", docNo,
            cmd.SellToCustomerNo, cmd.SellToCustomerName, cmd.SellToCustomerName,
            Utc(cmd.PostingDate));

        if (!headerResult.IsSuccess)
            throw new InvalidOperationException(headerResult.GetError()!.Message);

        var header = headerResult.GetValue()!;
        header.DueDate = UtcN(cmd.ValidUntilDate);
        header.ValidUntilDate = UtcN(cmd.ValidUntilDate);
        header.QuotedBy = cmd.QuotedBy ?? string.Empty;
        header.Observations = cmd.Observations ?? string.Empty;
        header.CurrencyCode = cmd.CurrencyCode ?? string.Empty;
        header.PaymentTermsCode = cmd.PaymentTermsCode ?? string.Empty;
        header.PaymentMethodCode = cmd.PaymentMethodCode ?? string.Empty;
        header.ExternalDocumentNo = cmd.ExternalDocumentNo ?? string.Empty;
        header.Status = "Open";

        decimal totalAmount = 0m;
        decimal totalAmountVat = 0m;
        int lineNo = 10000;

        var lineEntities = new List<SalesLine>();
        foreach (var l in cmd.Lines)
        {
            var lineResult = SalesLine.Create(tid, 0 /* Quote */, docNo, lineNo);
            if (!lineResult.IsSuccess) continue;

            var line = lineResult.GetValue()!;
            line.SellToCustomerNo = cmd.SellToCustomerNo;
            line.Type = l.LineType == "Service" ? (short)3 : (short)2;
            line.No = l.ItemNo ?? string.Empty;
            line.Description = l.Description;
            line.UnitOfMeasure = l.UnitOfMeasure ?? string.Empty;
            line.Quantity = l.Quantity;
            line.UnitPrice = l.UnitPrice;
            line.LineDiscount = l.LineDiscountPct;

            decimal vatRate = (l.VatPct > 0 ? l.VatPct : 18m) / 100m;
            line.Vat = vatRate * 100;

            decimal lineAmt = l.Quantity * l.UnitPrice;
            if (l.LineDiscountPct > 0)
            {
                decimal discAmt = lineAmt * (l.LineDiscountPct / 100m);
                line.LineDiscountAmount = Math.Round(discAmt, 2);
                lineAmt -= discAmt;
            }
            line.Amount = Math.Round(lineAmt, 2);
            line.AmountIncludingVat = Math.Round(lineAmt * (1 + vatRate), 2);
            line.OutstandingQuantity = l.Quantity;

            // Service-specific fields
            if (l.LineType == "Service")
            {
                line.ServiceBillingType = l.ServiceBillingType;
                line.ServiceStartDate = UtcN(l.ServiceStartDate);
                line.ServiceEndDate = UtcN(l.ServiceEndDate);
                line.ServiceHours = l.ServiceHours;
                line.HourlyRate = l.HourlyRate;
                line.ResourceNo = l.ResourceNo ?? string.Empty;

                // For estimated time: recalculate unit price from hours × rate
                if (l.ServiceBillingType == 2 && l.ServiceHours.HasValue && l.HourlyRate.HasValue)
                {
                    line.UnitPrice = l.HourlyRate.Value;
                    line.Quantity = l.ServiceHours.Value;
                    lineAmt = line.ServiceHours.Value * l.HourlyRate.Value;
                    if (l.LineDiscountPct > 0)
                        lineAmt -= Math.Round(lineAmt * (l.LineDiscountPct / 100m), 2);
                    line.Amount = Math.Round(lineAmt, 2);
                    line.AmountIncludingVat = Math.Round(lineAmt * (1 + vatRate), 2);
                }
            }

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
        return new CreateQuotationResult(docNo);
    }
}
