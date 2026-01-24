import { FiscalDocumentStatus, ClientStatus, ProductStatus } from '../../../../types';

interface BadgeProps {
  status: FiscalDocumentStatus | ClientStatus | ProductStatus | 'low-stock' | 'in-stock';
  children: React.ReactNode;
}

const statusStyles = {
  // Document Statuses
  active: 'bg-ink-paid-bg border-ink-paid-border text-ink-paid-text',
  inactive: 'bg-ink-error-bg border-ink-error-border text-ink-error-text',
  
  // Fiscal Document Statuses
  accepted: 'bg-ink-paid-bg border-ink-paid-border text-ink-paid-text',
  paid: 'bg-ink-paid-bg border-ink-paid-border text-ink-paid-text', // legacy support if needed
  
  draft: 'bg-matte-base border-matte-border text-matte-text-muted',
  signed: 'bg-ink-pending-bg border-ink-pending-border text-ink-pending-text',
  sent: 'bg-ink-pending-bg border-ink-pending-border text-ink-pending-text',
  
  rejected: 'bg-ink-error-bg border-ink-error-border text-ink-error-text',
  cancelled: 'bg-ink-error-bg border-ink-error-border text-ink-error-text',
  overdue: 'bg-ink-error-bg border-ink-error-border text-ink-error-text', // legacy support

  // Inventory
  'low-stock': 'bg-ink-pending-bg border-ink-pending-border text-ink-pending-text',
  'in-stock': 'bg-ink-paid-bg border-ink-paid-border text-ink-paid-text',
  'out-of-stock': 'bg-ink-error-bg border-ink-error-border text-ink-error-text',
};

export function Badge({ status, children }: BadgeProps) {
  const style = statusStyles[status as keyof typeof statusStyles] || 'bg-matte-base border-matte-border text-matte-text';
  
  return (
    <span
      className={`inline-flex items-center rounded-md border px-2 py-1 text-xs font-medium uppercase tracking-wide shadow-sm transition-all duration-200 ease-out hover:scale-105 ${style}`}
    >
      {children}
    </span>
  );
}
