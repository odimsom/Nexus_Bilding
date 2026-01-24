import { ReactNode } from 'react';

interface CardProps {
  children: ReactNode;
  className?: string;
}

export function Card({ children, className = '' }: CardProps) {
  return (
    <div className={`rounded-xl border border-matte-border bg-matte-surface p-6 shadow-sm transition-all duration-300 ease-out hover:shadow-md hover:-translate-y-0.5 ${className}`}>
      {children}
    </div>
  );
}
