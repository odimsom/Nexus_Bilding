import { Loader2 } from 'lucide-react';

interface LoadingStateProps {
  message?: string;
}

export function LoadingState({ message = 'Loading...' }: LoadingStateProps) {
  return (
    <div className="flex h-64 flex-col items-center justify-center gap-4 animate-in fade-in duration-500">
      <div className="relative">
        <div className="h-12 w-12 rounded-full border-4 border-matte-border bg-transparent"></div>
        <div className="absolute top-0 left-0 h-12 w-12 rounded-full border-4 border-primary border-t-transparent animate-spin"></div>
      </div>
      <p className="text-sm font-medium text-matte-text-muted animate-pulse">{message}</p>
    </div>
  );
}
