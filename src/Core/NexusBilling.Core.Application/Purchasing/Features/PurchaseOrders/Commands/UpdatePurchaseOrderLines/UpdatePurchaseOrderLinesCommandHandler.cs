using MediatR;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Interfaces.Repositories.Base;
using NexusBilling.Core.Domain.Purchasing.Entities;
using NexusBilling.Core.Domain.Purchasing.Repositories;

namespace NexusBilling.Core.Application.Purchasing.Features.PurchaseOrders.Commands.UpdatePurchaseOrderLines;

public sealed class UpdatePurchaseOrderLinesCommandHandler(
    IPurchaseHeaderRepository headerRepo,
    IPurchaseLineRepository lineRepo,
    IUnitOfWork uow)
    : IRequestHandler<UpdatePurchaseOrderLinesCommand, UpdatePurchaseOrderLinesResult>
{
    private const decimal VatRate = 0.18m;

    public async Task<UpdatePurchaseOrderLinesResult> Handle(UpdatePurchaseOrderLinesCommand cmd, CancellationToken ct)
    {
        var tid = TenantIdentifier.Create(cmd.TenantId);

        var header = await headerRepo.GetByNoAsync(cmd.OrderNo, ct)
            ?? throw new InvalidOperationException($"El pedido {cmd.OrderNo} no existe.");

        if (header.TenantId != tid)
            throw new InvalidOperationException($"El pedido {cmd.OrderNo} no existe.");

        if (header.Status != "Open")
            throw new InvalidOperationException($"El pedido {cmd.OrderNo} no está en estado Abierto.");

        var existing = await lineRepo.GetByDocumentNoAsync(1, cmd.OrderNo, ct);
        foreach (var line in existing)
            await lineRepo.DeleteAsync(line, ct);

        decimal totalAmount = 0m;
        decimal totalAmountVat = 0m;
        int lineNo = 10000;

        foreach (var l in cmd.Lines)
        {
            var lineResult = PurchaseLine.Create(
                tenantId: tid,
                documentType: 1,
                buyFromVendorNo: header.BuyFromVendorNo,
                documentNo: cmd.OrderNo,
                lineNo: lineNo,
                type: (short)(l.LineType switch { "G/L Account" => 1, "Service" => 3, _ => 2 }),
                no: l.ItemNo ?? string.Empty,
                locationCode: string.Empty,
                postingGroup: string.Empty,
                expectedReceiptDate: null,
                description: l.Description,
                description2: string.Empty,
                unitOfMeasure: l.UnitOfMeasure,
                quantity: l.Quantity,
                outstandingQuantity: l.Quantity,
                qtyToInvoice: l.Quantity,
                qtyToReceive: l.Quantity,
                directUnitCost: l.UnitPrice,
                unitCostLcy: l.UnitPrice,
                vat: VatRate * 100,
                lineDiscount: l.LineDiscountPct,
                lineDiscountAmount: 0m,
                amount: 0m,
                amountIncludingVat: 0m,
                unitPriceLcy: l.UnitPrice,
                allowInvoiceDisc: true,
                grossWeight: 0m, netWeight: 0m, unitsPerParcel: 0m, unitVolume: 0m,
                applToItemEntry: 0,
                shortcutDimension1Code: string.Empty, shortcutDimension2Code: string.Empty,
                jobNo: null, indirectCost: 0m, recalculateInvoiceDisc: false,
                outstandingAmount: 0m, qtyRcdNotInvoiced: 0m, amtRcdNotInvoiced: 0m,
                quantityReceived: 0m, quantityInvoiced: 0m,
                receiptNo: string.Empty, receiptLineNo: 0, profit: 0m,
                payToVendorNo: null, invDiscountAmount: 0m,
                vendorItemNo: string.Empty, salesOrderNo: string.Empty, salesOrderLineNo: 0,
                dropShipment: false,
                genBusPostingGroup: string.Empty, genProdPostingGroup: string.Empty,
                vatCalculationType: 0,
                transactionType: string.Empty, transportMethod: string.Empty,
                attachedToLineNo: 0, entryPoint: string.Empty, area: null,
                transactionSpecification: string.Empty,
                taxAreaCode: string.Empty, taxLiable: false, taxGroupCode: string.Empty, useTax: false,
                vatBusPostingGroup: string.Empty, vatProdPostingGroup: string.Empty,
                currencyCode: null,
                outstandingAmountLcy: 0m, amtRcdNotInvoicedLcy: 0m,
                blanketOrderNo: string.Empty, blanketOrderLineNo: 0,
                vatBaseAmount: 0m, unitCost: 0m, systemCreatedEntry: false,
                lineAmount: 0m, vatDifference: 0m, invDiscAmountToInvoice: 0m,
                vatIdentifier: string.Empty, icPartnerRefType: 0, icPartnerReference: string.Empty,
                prepayment: 0m, prepmtLineAmount: 0m, prepmtAmtInv: 0m, prepmtAmtInclVat: 0m,
                prepaymentAmount: 0m, prepmtVatBaseAmt: 0m, prepaymentVat: 0m, prepmtVatCalcType: 0,
                prepaymentVatIdentifier: string.Empty, prepaymentTaxAreaCode: string.Empty,
                prepaymentTaxLiable: false, prepaymentTaxGroupCode: string.Empty,
                prepmtAmtToDeduct: 0m, prepmtAmtDeducted: 0m, prepaymentLine: false,
                prepmtAmountInvInclVat: 0m, prepmtAmountInvLcy: 0m,
                icPartnerCode: string.Empty, prepmtVatAmountInvLcy: 0m,
                prepaymentVatDifference: 0m, prepmtVatDiffToDeduct: 0m, prepmtVatDiffDeducted: 0m,
                outstandingAmtExVatLcy: 0m, aRcdNotInvExVatLcy: 0m,
                dimensionSetId: 0, jobTaskNo: string.Empty, jobLineType: 0,
                jobUnitPrice: 0m, jobTotalPrice: 0m, jobLineAmount: 0m,
                jobLineDiscountAmount: 0m, jobLineDiscount: 0m,
                jobUnitPriceLcy: 0m, jobTotalPriceLcy: 0m, jobLineAmountLcy: 0m,
                jobLineDiscAmountLcy: 0m, jobCurrencyFactor: 0m, jobCurrencyCode: string.Empty,
                jobPlanningLineNo: 0, jobRemainingQty: 0m, jobRemainingQtyBase: 0m,
                deferralCode: string.Empty, returnsDeferralStartDate: null,
                prodOrderNo: string.Empty, variantCode: string.Empty, binCode: string.Empty,
                qtyPerUnitOfMeasure: 0m, unitOfMeasureCode: string.Empty,
                quantityBase: 0m, outstandingQtyBase: 0m, qtyToInvoiceBase: 0m,
                qtyToReceiveBase: 0m, qtyRcdNotInvoicedBase: 0m, qtyReceivedBase: 0m, qtyInvoicedBase: 0m,
                faPostingDate: null, faPostingType: 0,
                depreciationBookCode: string.Empty, salvageValue: 0m,
                deprUntilFaPostingDate: false, deprAcquisitionCost: false,
                maintenanceCode: null, insuranceNo: null,
                budgetedFaNo: string.Empty, duplicateInDepreciationBook: string.Empty,
                useDuplicationList: false, responsibilityCenter: string.Empty,
                crossReferenceNo: string.Empty, unitOfMeasureCrossRef: string.Empty,
                crossReferenceType: 0, crossReferenceTypeNo: string.Empty,
                itemCategoryCode: string.Empty, nonstock: false,
                purchasingCode: null, productGroupCode: string.Empty,
                specialOrder: false, specialOrderSalesNo: string.Empty, specialOrderSalesLineNo: 0,
                completelyReceived: false,
                requestedReceiptDate: null, promisedReceiptDate: null,
                leadTimeCalculation: string.Empty, inboundWhseHandlingTime: string.Empty,
                plannedReceiptDate: null, orderDate: null,
                allowItemChargeAssignment: false,
                returnQtyToShip: 0m, returnQtyToShipBase: 0m,
                returnQtyShippedNotInvd: 0m, retQtyShpdNotInvdBase: 0m,
                returnShpdNotInvd: 0m, returnShpdNotInvdLcy: 0m,
                returnQtyShipped: 0m, returnQtyShippedBase: 0m,
                returnShipmentNo: string.Empty, returnShipmentLineNo: 0, returnReasonCode: string.Empty,
                routingNo: string.Empty, operationNo: string.Empty, workCenterNo: string.Empty,
                finished: false, prodOrderLineNo: 0, overheadRate: 0m,
                mpsOrder: false, planningFlexibility: 0,
                safetyLeadTime: string.Empty, routingReferenceNo: 0
            );

            if (!lineResult.IsSuccess) continue;

            var line = lineResult.GetValue()!;

            decimal lineAmt = l.Quantity * l.UnitPrice;
            if (l.LineDiscountPct > 0)
            {
                decimal discAmt = lineAmt * (l.LineDiscountPct / 100m);
                lineAmt -= discAmt;
            }

            typeof(PurchaseLine).GetProperty("Amount")?.SetValue(line, Math.Round(lineAmt, 2));
            typeof(PurchaseLine).GetProperty("AmountIncludingVat")?.SetValue(line, Math.Round(lineAmt * (1 + VatRate), 2));
            typeof(PurchaseLine).GetProperty("OutstandingAmount")?.SetValue(line, Math.Round(lineAmt, 2));

            totalAmount += Math.Round(lineAmt, 2);
            totalAmountVat += Math.Round(lineAmt * (1 + VatRate), 2);

            await lineRepo.AddAsync(line, ct);
            lineNo += 10000;
        }

        header.Amount = Math.Round(totalAmount, 2);
        header.AmountIncludingVat = Math.Round(totalAmountVat, 2);
        await headerRepo.UpdateAsync(header, ct);
        await uow.SaveChangesAsync(ct);

        return new UpdatePurchaseOrderLinesResult(header.Amount, header.AmountIncludingVat);
    }
}
