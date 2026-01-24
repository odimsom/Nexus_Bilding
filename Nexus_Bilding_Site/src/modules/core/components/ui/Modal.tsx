import { ReactNode } from 'react';
import { X } from 'lucide-react';

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  title: string;
  children: ReactNode;
  icon?: ReactNode;
}

export function Modal({ isOpen, onClose, title, children, icon }: ModalProps) {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/60 backdrop-blur-[2px]">
      <div className="w-full max-w-md scale-100 transform rounded-xl border border-matte-border bg-matte-surface p-6 shadow-2xl transition-all">
        <div className="flex items-center justify-between mb-5">
          <h3 className="text-lg font-bold text-matte-text flex items-center gap-2">
            {icon}
            {title}
          </h3>
          <button
            onClick={onClose}
            className="flex items-center justify-center text-matte-text-muted hover:text-matte-text transition-colors"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {children}
      </div>
    </div>
  );
}
