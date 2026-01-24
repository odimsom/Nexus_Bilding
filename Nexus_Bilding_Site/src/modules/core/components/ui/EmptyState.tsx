import { ReactNode } from 'react';
import { Plus } from 'lucide-react';

interface EmptyStateProps {
  icon: ReactNode;
  title: string;
  description: string;
  actionLabel?: string;
  onAction?: () => void;
}

export function EmptyState({ icon, title, description, actionLabel, onAction }: EmptyStateProps) {
  return (
    <div className="flex flex-1 flex-col items-center justify-center p-8">
      <div className="flex max-w-md flex-col items-center justify-center text-center">
        <div className="mb-6 flex size-24 items-center justify-center rounded-full bg-matte-surface border border-matte-border/50">
          <div className="text-matte-border">
            {icon}
          </div>
        </div>

        <h3 className="mb-2 text-xl font-semibold text-matte-text-muted">{title}</h3>
        <p className="mb-8 text-sm text-matte-text-muted">{description}</p>

        {actionLabel && onAction && (
          <button
            onClick={onAction}
            className="flex items-center gap-2 rounded-lg bg-primary px-6 py-3 text-sm font-medium text-white shadow-sm transition-colors hover:bg-primary/90 focus:outline-none focus:ring-2 focus:ring-primary focus:ring-offset-2"
          >
            <Plus className="w-5 h-5" />
            {actionLabel}
          </button>
        )}
      </div>
    </div>
  );
}
