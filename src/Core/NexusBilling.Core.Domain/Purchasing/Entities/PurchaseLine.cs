using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Purchasing.Entities;

public class PurchaseLine : Entity
{
    private PurchaseLine() { }

    public TenantIdentifier TenantId { get; private set; }
    public short DocumentType { get; private set; }
    public string? BuyFromVendorNo { get; private set; }
    public string DocumentNo { get; private set; }
    public int LineNo { get; private set; }
    public short Type { get; private set; }
    public string No { get; private set; }
    public string LocationCode { get; private set; }
    public string PostingGroup { get; private set; }
    public DateTime? ExpectedReceiptDate { get; private set; }
    public string Description { get; private set; }
    public string Description2 { get; private set; }
    public string UnitOfMeasure { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal OutstandingQuantity { get; private set; }
    public decimal QtyToInvoice { get; private set; }
    public decimal QtyToReceive { get; private set; }
    public decimal DirectUnitCost { get; private set; }
    public decimal UnitCostLcy { get; private set; }
    public decimal Vat { get; private set; }
    public decimal LineDiscount { get; private set; }
    public decimal LineDiscountAmount { get; private set; }
    public decimal Amount { get; private set; }
    public decimal AmountIncludingVat { get; private set; }
    public decimal UnitPriceLcy { get; private set; }
    public bool AllowInvoiceDisc { get; private set; }
    public decimal GrossWeight { get; private set; }
    public decimal NetWeight { get; private set; }
    public decimal UnitsPerParcel { get; private set; }
    public decimal UnitVolume { get; private set; }
    public int ApplToItemEntry { get; private set; }
    public string ShortcutDimension1Code { get; private set; }
    public string ShortcutDimension2Code { get; private set; }
    public string? JobNo { get; private set; }
    public decimal IndirectCost { get; private set; }
    public bool RecalculateInvoiceDisc { get; private set; }
    public decimal OutstandingAmount { get; private set; }
    public decimal QtyRcdNotInvoiced { get; private set; }
    public decimal AmtRcdNotInvoiced { get; private set; }
    public decimal QuantityReceived { get; private set; }
    public decimal QuantityInvoiced { get; private set; }
    public string ReceiptNo { get; private set; }
    public int ReceiptLineNo { get; private set; }
    public decimal Profit { get; private set; }
    public string? PayToVendorNo { get; private set; }
    public decimal InvDiscountAmount { get; private set; }
    public string VendorItemNo { get; private set; }
    public string SalesOrderNo { get; private set; }
    public int SalesOrderLineNo { get; private set; }
    public bool DropShipment { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public short VatCalculationType { get; private set; }
    public string TransactionType { get; private set; }
    public string TransportMethod { get; private set; }
    public int AttachedToLineNo { get; private set; }
    public string EntryPoint { get; private set; }
    public string? Area { get; private set; }
    public string TransactionSpecification { get; private set; }
    public string TaxAreaCode { get; private set; }
    public bool TaxLiable { get; private set; }
    public string TaxGroupCode { get; private set; }
    public bool UseTax { get; private set; }
    public string VatBusPostingGroup { get; private set; }
    public string VatProdPostingGroup { get; private set; }
    public string? CurrencyCode { get; private set; }
    public decimal OutstandingAmountLcy { get; private set; }
    public decimal AmtRcdNotInvoicedLcy { get; private set; }
    public string BlanketOrderNo { get; private set; }
    public int BlanketOrderLineNo { get; private set; }
    public decimal VatBaseAmount { get; private set; }
    public decimal UnitCost { get; private set; }
    public bool SystemCreatedEntry { get; private set; }
    public decimal LineAmount { get; private set; }
    public decimal VatDifference { get; private set; }
    public decimal InvDiscAmountToInvoice { get; private set; }
    public string VatIdentifier { get; private set; }
    public short IcPartnerRefType { get; private set; }
    public string IcPartnerReference { get; private set; }
    public decimal Prepayment { get; private set; }
    public decimal PrepmtLineAmount { get; private set; }
    public decimal PrepmtAmtInv { get; private set; }
    public decimal PrepmtAmtInclVat { get; private set; }
    public decimal PrepaymentAmount { get; private set; }
    public decimal PrepmtVatBaseAmt { get; private set; }
    public decimal PrepaymentVat { get; private set; }
    public short PrepmtVatCalcType { get; private set; }
    public string PrepaymentVatIdentifier { get; private set; }
    public string PrepaymentTaxAreaCode { get; private set; }
    public bool PrepaymentTaxLiable { get; private set; }
    public string PrepaymentTaxGroupCode { get; private set; }
    public decimal PrepmtAmtToDeduct { get; private set; }
    public decimal PrepmtAmtDeducted { get; private set; }
    public bool PrepaymentLine { get; private set; }
    public decimal PrepmtAmountInvInclVat { get; private set; }
    public decimal PrepmtAmountInvLcy { get; private set; }
    public string IcPartnerCode { get; private set; }
    public decimal PrepmtVatAmountInvLcy { get; private set; }
    public decimal PrepaymentVatDifference { get; private set; }
    public decimal PrepmtVatDiffToDeduct { get; private set; }
    public decimal PrepmtVatDiffDeducted { get; private set; }
    public decimal OutstandingAmtExVatLcy { get; private set; }
    public decimal ARcdNotInvExVatLcy { get; private set; }
    public int DimensionSetId { get; private set; }
    public string JobTaskNo { get; private set; }
    public short JobLineType { get; private set; }
    public decimal JobUnitPrice { get; private set; }
    public decimal JobTotalPrice { get; private set; }
    public decimal JobLineAmount { get; private set; }
    public decimal JobLineDiscountAmount { get; private set; }
    public decimal JobLineDiscount { get; private set; }
    public decimal JobUnitPriceLcy { get; private set; }
    public decimal JobTotalPriceLcy { get; private set; }
    public decimal JobLineAmountLcy { get; private set; }
    public decimal JobLineDiscAmountLcy { get; private set; }
    public decimal JobCurrencyFactor { get; private set; }
    public string JobCurrencyCode { get; private set; }
    public int JobPlanningLineNo { get; private set; }
    public decimal JobRemainingQty { get; private set; }
    public decimal JobRemainingQtyBase { get; private set; }
    public string DeferralCode { get; private set; }
    public DateTime? ReturnsDeferralStartDate { get; private set; }
    public string ProdOrderNo { get; private set; }
    public string VariantCode { get; private set; }
    public string BinCode { get; private set; }
    public decimal QtyPerUnitOfMeasure { get; private set; }
    public string UnitOfMeasureCode { get; private set; }
    public decimal QuantityBase { get; private set; }
    public decimal OutstandingQtyBase { get; private set; }
    public decimal QtyToInvoiceBase { get; private set; }
    public decimal QtyToReceiveBase { get; private set; }
    public decimal QtyRcdNotInvoicedBase { get; private set; }
    public decimal QtyReceivedBase { get; private set; }
    public decimal QtyInvoicedBase { get; private set; }
    public DateTime? FaPostingDate { get; private set; }
    public short FaPostingType { get; private set; }
    public string DepreciationBookCode { get; private set; }
    public decimal SalvageValue { get; private set; }
    public bool DeprUntilFaPostingDate { get; private set; }
    public bool DeprAcquisitionCost { get; private set; }
    public string? MaintenanceCode { get; private set; }
    public string? InsuranceNo { get; private set; }
    public string BudgetedFaNo { get; private set; }
    public string DuplicateInDepreciationBook { get; private set; }
    public bool UseDuplicationList { get; private set; }
    public string ResponsibilityCenter { get; private set; }
    public string CrossReferenceNo { get; private set; }
    public string UnitOfMeasureCrossRef { get; private set; }
    public short CrossReferenceType { get; private set; }
    public string CrossReferenceTypeNo { get; private set; }
    public string ItemCategoryCode { get; private set; }
    public bool Nonstock { get; private set; }
    public string? PurchasingCode { get; private set; }
    public string ProductGroupCode { get; private set; }
    public bool SpecialOrder { get; private set; }
    public string SpecialOrderSalesNo { get; private set; }
    public int SpecialOrderSalesLineNo { get; private set; }
    public bool CompletelyReceived { get; private set; }
    public DateTime? RequestedReceiptDate { get; private set; }
    public DateTime? PromisedReceiptDate { get; private set; }
    public string LeadTimeCalculation { get; private set; }
    public string InboundWhseHandlingTime { get; private set; }
    public DateTime? PlannedReceiptDate { get; private set; }
    public DateTime? OrderDate { get; private set; }
    public bool AllowItemChargeAssignment { get; private set; }
    public decimal ReturnQtyToShip { get; private set; }
    public decimal ReturnQtyToShipBase { get; private set; }
    public decimal ReturnQtyShippedNotInvd { get; private set; }
    public decimal RetQtyShpdNotInvdBase { get; private set; }
    public decimal ReturnShpdNotInvd { get; private set; }
    public decimal ReturnShpdNotInvdLcy { get; private set; }
    public decimal ReturnQtyShipped { get; private set; }
    public decimal ReturnQtyShippedBase { get; private set; }
    public string ReturnShipmentNo { get; private set; }
    public int ReturnShipmentLineNo { get; private set; }
    public string ReturnReasonCode { get; private set; }
    public string RoutingNo { get; private set; }
    public string OperationNo { get; private set; }
    public string WorkCenterNo { get; private set; }
    public bool Finished { get; private set; }
    public int ProdOrderLineNo { get; private set; }
    public decimal OverheadRate { get; private set; }
    public bool MpsOrder { get; private set; }
    public short PlanningFlexibility { get; private set; }
    public string SafetyLeadTime { get; private set; }
    public int RoutingReferenceNo { get; private set; }

    public static OperationResult<PurchaseLine, DomainError> Create(
        TenantIdentifier tenantId,
        short documentType,
        string? buyFromVendorNo,
        string documentNo,
        int lineNo,
        short type,
        string no,
        string locationCode,
        string postingGroup,
        DateTime? expectedReceiptDate,
        string description,
        string description2,
        string unitOfMeasure,
        decimal quantity,
        decimal outstandingQuantity,
        decimal qtyToInvoice,
        decimal qtyToReceive,
        decimal directUnitCost,
        decimal unitCostLcy,
        decimal vat,
        decimal lineDiscount,
        decimal lineDiscountAmount,
        decimal amount,
        decimal amountIncludingVat,
        decimal unitPriceLcy,
        bool allowInvoiceDisc,
        decimal grossWeight,
        decimal netWeight,
        decimal unitsPerParcel,
        decimal unitVolume,
        int applToItemEntry,
        string shortcutDimension1Code,
        string shortcutDimension2Code,
        string? jobNo,
        decimal indirectCost,
        bool recalculateInvoiceDisc,
        decimal outstandingAmount,
        decimal qtyRcdNotInvoiced,
        decimal amtRcdNotInvoiced,
        decimal quantityReceived,
        decimal quantityInvoiced,
        string receiptNo,
        int receiptLineNo,
        decimal profit,
        string? payToVendorNo,
        decimal invDiscountAmount,
        string vendorItemNo,
        string salesOrderNo,
        int salesOrderLineNo,
        bool dropShipment,
        string genBusPostingGroup,
        string genProdPostingGroup,
        short vatCalculationType,
        string transactionType,
        string transportMethod,
        int attachedToLineNo,
        string entryPoint,
        string? area,
        string transactionSpecification,
        string taxAreaCode,
        bool taxLiable,
        string taxGroupCode,
        bool useTax,
        string vatBusPostingGroup,
        string vatProdPostingGroup,
        string? currencyCode,
        decimal outstandingAmountLcy,
        decimal amtRcdNotInvoicedLcy,
        string blanketOrderNo,
        int blanketOrderLineNo,
        decimal vatBaseAmount,
        decimal unitCost,
        bool systemCreatedEntry,
        decimal lineAmount,
        decimal vatDifference,
        decimal invDiscAmountToInvoice,
        string vatIdentifier,
        short icPartnerRefType,
        string icPartnerReference,
        decimal prepayment,
        decimal prepmtLineAmount,
        decimal prepmtAmtInv,
        decimal prepmtAmtInclVat,
        decimal prepaymentAmount,
        decimal prepmtVatBaseAmt,
        decimal prepaymentVat,
        short prepmtVatCalcType,
        string prepaymentVatIdentifier,
        string prepaymentTaxAreaCode,
        bool prepaymentTaxLiable,
        string prepaymentTaxGroupCode,
        decimal prepmtAmtToDeduct,
        decimal prepmtAmtDeducted,
        bool prepaymentLine,
        decimal prepmtAmountInvInclVat,
        decimal prepmtAmountInvLcy,
        string icPartnerCode,
        decimal prepmtVatAmountInvLcy,
        decimal prepaymentVatDifference,
        decimal prepmtVatDiffToDeduct,
        decimal prepmtVatDiffDeducted,
        decimal outstandingAmtExVatLcy,
        decimal aRcdNotInvExVatLcy,
        int dimensionSetId,
        string jobTaskNo,
        short jobLineType,
        decimal jobUnitPrice,
        decimal jobTotalPrice,
        decimal jobLineAmount,
        decimal jobLineDiscountAmount,
        decimal jobLineDiscount,
        decimal jobUnitPriceLcy,
        decimal jobTotalPriceLcy,
        decimal jobLineAmountLcy,
        decimal jobLineDiscAmountLcy,
        decimal jobCurrencyFactor,
        string jobCurrencyCode,
        int jobPlanningLineNo,
        decimal jobRemainingQty,
        decimal jobRemainingQtyBase,
        string deferralCode,
        DateTime? returnsDeferralStartDate,
        string prodOrderNo,
        string variantCode,
        string binCode,
        decimal qtyPerUnitOfMeasure,
        string unitOfMeasureCode,
        decimal quantityBase,
        decimal outstandingQtyBase,
        decimal qtyToInvoiceBase,
        decimal qtyToReceiveBase,
        decimal qtyRcdNotInvoicedBase,
        decimal qtyReceivedBase,
        decimal qtyInvoicedBase,
        DateTime? faPostingDate,
        short faPostingType,
        string depreciationBookCode,
        decimal salvageValue,
        bool deprUntilFaPostingDate,
        bool deprAcquisitionCost,
        string? maintenanceCode,
        string? insuranceNo,
        string budgetedFaNo,
        string duplicateInDepreciationBook,
        bool useDuplicationList,
        string responsibilityCenter,
        string crossReferenceNo,
        string unitOfMeasureCrossRef,
        short crossReferenceType,
        string crossReferenceTypeNo,
        string itemCategoryCode,
        bool nonstock,
        string? purchasingCode,
        string productGroupCode,
        bool specialOrder,
        string specialOrderSalesNo,
        int specialOrderSalesLineNo,
        bool completelyReceived,
        DateTime? requestedReceiptDate,
        DateTime? promisedReceiptDate,
        string leadTimeCalculation,
        string inboundWhseHandlingTime,
        DateTime? plannedReceiptDate,
        DateTime? orderDate,
        bool allowItemChargeAssignment,
        decimal returnQtyToShip,
        decimal returnQtyToShipBase,
        decimal returnQtyShippedNotInvd,
        decimal retQtyShpdNotInvdBase,
        decimal returnShpdNotInvd,
        decimal returnShpdNotInvdLcy,
        decimal returnQtyShipped,
        decimal returnQtyShippedBase,
        string returnShipmentNo,
        int returnShipmentLineNo,
        string returnReasonCode,
        string routingNo,
        string operationNo,
        string workCenterNo,
        bool finished,
        int prodOrderLineNo,
        decimal overheadRate,
        bool mpsOrder,
        short planningFlexibility,
        string safetyLeadTime,
        int routingReferenceNo)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<PurchaseLine, DomainError>.Fail(DomainError.Validation("purchasing.purchase_line.tenant_id_required", "TenantId is required."));
        if (string.IsNullOrWhiteSpace(documentNo))
            return OperationResult<PurchaseLine, DomainError>.Fail(DomainError.Validation("purchasing.purchase_line.document_no_required", "DocumentNo is required."));

        var entity = new PurchaseLine
        {
            TenantId = tenantId,
            DocumentType = documentType,
            BuyFromVendorNo = buyFromVendorNo,
            DocumentNo = documentNo.Trim(),
            LineNo = lineNo,
            Type = type,
            No = no ?? string.Empty,
            LocationCode = locationCode ?? string.Empty,
            PostingGroup = postingGroup ?? string.Empty,
            ExpectedReceiptDate = expectedReceiptDate,
            Description = description ?? string.Empty,
            Description2 = description2 ?? string.Empty,
            UnitOfMeasure = unitOfMeasure ?? string.Empty,
            Quantity = quantity,
            OutstandingQuantity = outstandingQuantity,
            QtyToInvoice = qtyToInvoice,
            QtyToReceive = qtyToReceive,
            DirectUnitCost = directUnitCost,
            UnitCostLcy = unitCostLcy,
            Vat = vat,
            LineDiscount = lineDiscount,
            LineDiscountAmount = lineDiscountAmount,
            Amount = amount,
            AmountIncludingVat = amountIncludingVat,
            UnitPriceLcy = unitPriceLcy,
            AllowInvoiceDisc = allowInvoiceDisc,
            GrossWeight = grossWeight,
            NetWeight = netWeight,
            UnitsPerParcel = unitsPerParcel,
            UnitVolume = unitVolume,
            ApplToItemEntry = applToItemEntry,
            ShortcutDimension1Code = shortcutDimension1Code ?? string.Empty,
            ShortcutDimension2Code = shortcutDimension2Code ?? string.Empty,
            JobNo = jobNo,
            IndirectCost = indirectCost,
            RecalculateInvoiceDisc = recalculateInvoiceDisc,
            OutstandingAmount = outstandingAmount,
            QtyRcdNotInvoiced = qtyRcdNotInvoiced,
            AmtRcdNotInvoiced = amtRcdNotInvoiced,
            QuantityReceived = quantityReceived,
            QuantityInvoiced = quantityInvoiced,
            ReceiptNo = receiptNo ?? string.Empty,
            ReceiptLineNo = receiptLineNo,
            Profit = profit,
            PayToVendorNo = payToVendorNo,
            InvDiscountAmount = invDiscountAmount,
            VendorItemNo = vendorItemNo ?? string.Empty,
            SalesOrderNo = salesOrderNo ?? string.Empty,
            SalesOrderLineNo = salesOrderLineNo,
            DropShipment = dropShipment,
            GenBusPostingGroup = genBusPostingGroup ?? string.Empty,
            GenProdPostingGroup = genProdPostingGroup ?? string.Empty,
            VatCalculationType = vatCalculationType,
            TransactionType = transactionType ?? string.Empty,
            TransportMethod = transportMethod ?? string.Empty,
            AttachedToLineNo = attachedToLineNo,
            EntryPoint = entryPoint ?? string.Empty,
            Area = area,
            TransactionSpecification = transactionSpecification ?? string.Empty,
            TaxAreaCode = taxAreaCode ?? string.Empty,
            TaxLiable = taxLiable,
            TaxGroupCode = taxGroupCode ?? string.Empty,
            UseTax = useTax,
            VatBusPostingGroup = vatBusPostingGroup ?? string.Empty,
            VatProdPostingGroup = vatProdPostingGroup ?? string.Empty,
            CurrencyCode = currencyCode,
            OutstandingAmountLcy = outstandingAmountLcy,
            AmtRcdNotInvoicedLcy = amtRcdNotInvoicedLcy,
            BlanketOrderNo = blanketOrderNo ?? string.Empty,
            BlanketOrderLineNo = blanketOrderLineNo,
            VatBaseAmount = vatBaseAmount,
            UnitCost = unitCost,
            SystemCreatedEntry = systemCreatedEntry,
            LineAmount = lineAmount,
            VatDifference = vatDifference,
            InvDiscAmountToInvoice = invDiscAmountToInvoice,
            VatIdentifier = vatIdentifier ?? string.Empty,
            IcPartnerRefType = icPartnerRefType,
            IcPartnerReference = icPartnerReference ?? string.Empty,
            Prepayment = prepayment,
            PrepmtLineAmount = prepmtLineAmount,
            PrepmtAmtInv = prepmtAmtInv,
            PrepmtAmtInclVat = prepmtAmtInclVat,
            PrepaymentAmount = prepaymentAmount,
            PrepmtVatBaseAmt = prepmtVatBaseAmt,
            PrepaymentVat = prepaymentVat,
            PrepmtVatCalcType = prepmtVatCalcType,
            PrepaymentVatIdentifier = prepaymentVatIdentifier ?? string.Empty,
            PrepaymentTaxAreaCode = prepaymentTaxAreaCode ?? string.Empty,
            PrepaymentTaxLiable = prepaymentTaxLiable,
            PrepaymentTaxGroupCode = prepaymentTaxGroupCode ?? string.Empty,
            PrepmtAmtToDeduct = prepmtAmtToDeduct,
            PrepmtAmtDeducted = prepmtAmtDeducted,
            PrepaymentLine = prepaymentLine,
            PrepmtAmountInvInclVat = prepmtAmountInvInclVat,
            PrepmtAmountInvLcy = prepmtAmountInvLcy,
            IcPartnerCode = icPartnerCode ?? string.Empty,
            PrepmtVatAmountInvLcy = prepmtVatAmountInvLcy,
            PrepaymentVatDifference = prepaymentVatDifference,
            PrepmtVatDiffToDeduct = prepmtVatDiffToDeduct,
            PrepmtVatDiffDeducted = prepmtVatDiffDeducted,
            OutstandingAmtExVatLcy = outstandingAmtExVatLcy,
            ARcdNotInvExVatLcy = aRcdNotInvExVatLcy,
            DimensionSetId = dimensionSetId,
            JobTaskNo = jobTaskNo ?? string.Empty,
            JobLineType = jobLineType,
            JobUnitPrice = jobUnitPrice,
            JobTotalPrice = jobTotalPrice,
            JobLineAmount = jobLineAmount,
            JobLineDiscountAmount = jobLineDiscountAmount,
            JobLineDiscount = jobLineDiscount,
            JobUnitPriceLcy = jobUnitPriceLcy,
            JobTotalPriceLcy = jobTotalPriceLcy,
            JobLineAmountLcy = jobLineAmountLcy,
            JobLineDiscAmountLcy = jobLineDiscAmountLcy,
            JobCurrencyFactor = jobCurrencyFactor,
            JobCurrencyCode = jobCurrencyCode ?? string.Empty,
            JobPlanningLineNo = jobPlanningLineNo,
            JobRemainingQty = jobRemainingQty,
            JobRemainingQtyBase = jobRemainingQtyBase,
            DeferralCode = deferralCode ?? string.Empty,
            ReturnsDeferralStartDate = returnsDeferralStartDate,
            ProdOrderNo = prodOrderNo ?? string.Empty,
            VariantCode = variantCode ?? string.Empty,
            BinCode = binCode ?? string.Empty,
            QtyPerUnitOfMeasure = qtyPerUnitOfMeasure,
            UnitOfMeasureCode = unitOfMeasureCode ?? string.Empty,
            QuantityBase = quantityBase,
            OutstandingQtyBase = outstandingQtyBase,
            QtyToInvoiceBase = qtyToInvoiceBase,
            QtyToReceiveBase = qtyToReceiveBase,
            QtyRcdNotInvoicedBase = qtyRcdNotInvoicedBase,
            QtyReceivedBase = qtyReceivedBase,
            QtyInvoicedBase = qtyInvoicedBase,
            FaPostingDate = faPostingDate,
            FaPostingType = faPostingType,
            DepreciationBookCode = depreciationBookCode ?? string.Empty,
            SalvageValue = salvageValue,
            DeprUntilFaPostingDate = deprUntilFaPostingDate,
            DeprAcquisitionCost = deprAcquisitionCost,
            MaintenanceCode = maintenanceCode,
            InsuranceNo = insuranceNo,
            BudgetedFaNo = budgetedFaNo ?? string.Empty,
            DuplicateInDepreciationBook = duplicateInDepreciationBook ?? string.Empty,
            UseDuplicationList = useDuplicationList,
            ResponsibilityCenter = responsibilityCenter ?? string.Empty,
            CrossReferenceNo = crossReferenceNo ?? string.Empty,
            UnitOfMeasureCrossRef = unitOfMeasureCrossRef ?? string.Empty,
            CrossReferenceType = crossReferenceType,
            CrossReferenceTypeNo = crossReferenceTypeNo ?? string.Empty,
            ItemCategoryCode = itemCategoryCode ?? string.Empty,
            Nonstock = nonstock,
            PurchasingCode = purchasingCode,
            ProductGroupCode = productGroupCode ?? string.Empty,
            SpecialOrder = specialOrder,
            SpecialOrderSalesNo = specialOrderSalesNo ?? string.Empty,
            SpecialOrderSalesLineNo = specialOrderSalesLineNo,
            CompletelyReceived = completelyReceived,
            RequestedReceiptDate = requestedReceiptDate,
            PromisedReceiptDate = promisedReceiptDate,
            LeadTimeCalculation = leadTimeCalculation ?? string.Empty,
            InboundWhseHandlingTime = inboundWhseHandlingTime ?? string.Empty,
            PlannedReceiptDate = plannedReceiptDate,
            OrderDate = orderDate,
            AllowItemChargeAssignment = allowItemChargeAssignment,
            ReturnQtyToShip = returnQtyToShip,
            ReturnQtyToShipBase = returnQtyToShipBase,
            ReturnQtyShippedNotInvd = returnQtyShippedNotInvd,
            RetQtyShpdNotInvdBase = retQtyShpdNotInvdBase,
            ReturnShpdNotInvd = returnShpdNotInvd,
            ReturnShpdNotInvdLcy = returnShpdNotInvdLcy,
            ReturnQtyShipped = returnQtyShipped,
            ReturnQtyShippedBase = returnQtyShippedBase,
            ReturnShipmentNo = returnShipmentNo ?? string.Empty,
            ReturnShipmentLineNo = returnShipmentLineNo,
            ReturnReasonCode = returnReasonCode ?? string.Empty,
            RoutingNo = routingNo ?? string.Empty,
            OperationNo = operationNo ?? string.Empty,
            WorkCenterNo = workCenterNo ?? string.Empty,
            Finished = finished,
            ProdOrderLineNo = prodOrderLineNo,
            OverheadRate = overheadRate,
            MpsOrder = mpsOrder,
            PlanningFlexibility = planningFlexibility,
            SafetyLeadTime = safetyLeadTime ?? string.Empty,
            RoutingReferenceNo = routingReferenceNo
        };

        return OperationResult<PurchaseLine, DomainError>.Ok(entity);
    }
}
