using System;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Result;
using NexusBilling.Core.Domain.Common.Errors;

namespace NexusBilling.Core.Domain.Finance.Entities;

public class GeneralPostingSetup : Entity
{
    private GeneralPostingSetup() { }

    public TenantIdentifier TenantId { get; private set; }
    public string GenBusPostingGroup { get; private set; }
    public string GenProdPostingGroup { get; private set; }
    public string SalesAccount { get; private set; }
    public string SalesLineDiscAccount { get; private set; }
    public string SalesInvDiscAccount { get; private set; }
    public string SalesPmtDiscDebitAcc { get; private set; }
    public string PurchAccount { get; private set; }
    public string PurchLineDiscAccount { get; private set; }
    public string PurchInvDiscAccount { get; private set; }
    public string PurchPmtDiscCreditAcc { get; private set; }
    public string CogsAccount { get; private set; }
    public string InventoryAdjmtAccount { get; private set; }
    public string SalesCreditMemoAccount { get; private set; }
    public string PurchCreditMemoAccount { get; private set; }
    public string SalesPmtDiscCreditAcc { get; private set; }
    public string PurchPmtDiscDebitAcc { get; private set; }
    public string SalesPmtTolDebitAcc { get; private set; }
    public string SalesPmtTolCreditAcc { get; private set; }
    public string PurchPmtTolDebitAcc { get; private set; }
    public string PurchPmtTolCreditAcc { get; private set; }
    public string SalesPrepaymentsAccount { get; private set; }
    public string PurchPrepaymentsAccount { get; private set; }
    public string PurchFaDiscAccount { get; private set; }
    public string InvtAccrualAccInterim { get; private set; }
    public string CogsAccountInterim { get; private set; }
    public string DirectCostAppliedAccount { get; private set; }
    public string OverheadAppliedAccount { get; private set; }
    public string PurchaseVarianceAccount { get; private set; }

    public static OperationResult<GeneralPostingSetup, DomainError> Create(
        TenantIdentifier tenantId,
        string genBusPostingGroup,
        string genProdPostingGroup,
        string salesAccount,
        string salesLineDiscAccount,
        string salesInvDiscAccount,
        string salesPmtDiscDebitAcc,
        string purchAccount,
        string purchLineDiscAccount,
        string purchInvDiscAccount,
        string purchPmtDiscCreditAcc,
        string cogsAccount,
        string inventoryAdjmtAccount,
        string salesCreditMemoAccount,
        string purchCreditMemoAccount,
        string salesPmtDiscCreditAcc,
        string purchPmtDiscDebitAcc,
        string salesPmtTolDebitAcc,
        string salesPmtTolCreditAcc,
        string purchPmtTolDebitAcc,
        string purchPmtTolCreditAcc,
        string salesPrepaymentsAccount,
        string purchPrepaymentsAccount,
        string purchFaDiscAccount,
        string invtAccrualAccInterim,
        string cogsAccountInterim,
        string directCostAppliedAccount,
        string overheadAppliedAccount,
        string purchaseVarianceAccount)
    {
        if (tenantId == null || tenantId.Value == Guid.Empty)
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.tenant_required", "El TenantIdentifier es obligatorio."));
        if (string.IsNullOrWhiteSpace(genBusPostingGroup))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.gen_bus_posting_group_required", "El campo gen_bus_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(genProdPostingGroup))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.gen_prod_posting_group_required", "El campo gen_prod_posting_group es obligatorio."));
        if (string.IsNullOrWhiteSpace(salesAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.sales_account_required", "El campo sales_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(salesLineDiscAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.sales_line_disc_account_required", "El campo sales_line_disc_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(salesInvDiscAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.sales_inv_disc_account_required", "El campo sales_inv_disc_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(salesPmtDiscDebitAcc))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.sales_pmt_disc_debit_acc_required", "El campo sales_pmt_disc_debit_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(purchAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.purch_account_required", "El campo purch_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(purchLineDiscAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.purch_line_disc_account_required", "El campo purch_line_disc_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(purchInvDiscAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.purch_inv_disc_account_required", "El campo purch_inv_disc_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(purchPmtDiscCreditAcc))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.purch_pmt_disc_credit_acc_required", "El campo purch_pmt_disc_credit_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(cogsAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.cogs_account_required", "El campo cogs_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(inventoryAdjmtAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.inventory_adjmt_account_required", "El campo inventory_adjmt_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(salesCreditMemoAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.sales_credit_memo_account_required", "El campo sales_credit_memo_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(purchCreditMemoAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.purch_credit_memo_account_required", "El campo purch_credit_memo_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(salesPmtDiscCreditAcc))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.sales_pmt_disc_credit_acc_required", "El campo sales_pmt_disc_credit_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(purchPmtDiscDebitAcc))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.purch_pmt_disc_debit_acc_required", "El campo purch_pmt_disc_debit_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(salesPmtTolDebitAcc))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.sales_pmt_tol_debit_acc_required", "El campo sales_pmt_tol_debit_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(salesPmtTolCreditAcc))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.sales_pmt_tol_credit_acc_required", "El campo sales_pmt_tol_credit_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(purchPmtTolDebitAcc))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.purch_pmt_tol_debit_acc_required", "El campo purch_pmt_tol_debit_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(purchPmtTolCreditAcc))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.purch_pmt_tol_credit_acc_required", "El campo purch_pmt_tol_credit_acc es obligatorio."));
        if (string.IsNullOrWhiteSpace(salesPrepaymentsAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.sales_prepayments_account_required", "El campo sales_prepayments_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(purchPrepaymentsAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.purch_prepayments_account_required", "El campo purch_prepayments_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(purchFaDiscAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.purch_fa_disc_account_required", "El campo purch_fa_disc_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(invtAccrualAccInterim))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.invt_accrual_acc_interim_required", "El campo invt_accrual_acc_interim es obligatorio."));
        if (string.IsNullOrWhiteSpace(cogsAccountInterim))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.cogs_account_interim_required", "El campo cogs_account_interim es obligatorio."));
        if (string.IsNullOrWhiteSpace(directCostAppliedAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.direct_cost_applied_account_required", "El campo direct_cost_applied_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(overheadAppliedAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.overhead_applied_account_required", "El campo overhead_applied_account es obligatorio."));
        if (string.IsNullOrWhiteSpace(purchaseVarianceAccount))
            return OperationResult<GeneralPostingSetup, DomainError>.Fail(DomainError.Validation("finance.purchase_variance_account_required", "El campo purchase_variance_account es obligatorio."));

        var entity = new GeneralPostingSetup()
        {
            TenantId = tenantId,
            GenBusPostingGroup = genBusPostingGroup.Trim(),
            GenProdPostingGroup = genProdPostingGroup.Trim(),
            SalesAccount = salesAccount.Trim(),
            SalesLineDiscAccount = salesLineDiscAccount.Trim(),
            SalesInvDiscAccount = salesInvDiscAccount.Trim(),
            SalesPmtDiscDebitAcc = salesPmtDiscDebitAcc.Trim(),
            PurchAccount = purchAccount.Trim(),
            PurchLineDiscAccount = purchLineDiscAccount.Trim(),
            PurchInvDiscAccount = purchInvDiscAccount.Trim(),
            PurchPmtDiscCreditAcc = purchPmtDiscCreditAcc.Trim(),
            CogsAccount = cogsAccount.Trim(),
            InventoryAdjmtAccount = inventoryAdjmtAccount.Trim(),
            SalesCreditMemoAccount = salesCreditMemoAccount.Trim(),
            PurchCreditMemoAccount = purchCreditMemoAccount.Trim(),
            SalesPmtDiscCreditAcc = salesPmtDiscCreditAcc.Trim(),
            PurchPmtDiscDebitAcc = purchPmtDiscDebitAcc.Trim(),
            SalesPmtTolDebitAcc = salesPmtTolDebitAcc.Trim(),
            SalesPmtTolCreditAcc = salesPmtTolCreditAcc.Trim(),
            PurchPmtTolDebitAcc = purchPmtTolDebitAcc.Trim(),
            PurchPmtTolCreditAcc = purchPmtTolCreditAcc.Trim(),
            SalesPrepaymentsAccount = salesPrepaymentsAccount.Trim(),
            PurchPrepaymentsAccount = purchPrepaymentsAccount.Trim(),
            PurchFaDiscAccount = purchFaDiscAccount.Trim(),
            InvtAccrualAccInterim = invtAccrualAccInterim.Trim(),
            CogsAccountInterim = cogsAccountInterim.Trim(),
            DirectCostAppliedAccount = directCostAppliedAccount.Trim(),
            OverheadAppliedAccount = overheadAppliedAccount.Trim(),
            PurchaseVarianceAccount = purchaseVarianceAccount.Trim(),
        };

        return OperationResult<GeneralPostingSetup, DomainError>.Ok(entity);
    }
}
