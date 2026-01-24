import { X } from 'lucide-react';
import { Card } from '../../../core/components/ui/Card';

interface NotificationsModalProps {
  isOpen: boolean;
  onClose: () => void;
}

export function NotificationsModal({ isOpen, onClose }: NotificationsModalProps) {
  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-end justify-end sm:items-start sm:justify-end sm:pt-20 sm:pr-8 pointer-events-none">
      <div className="w-full max-w-sm pointer-events-auto transform transition-all duration-300 ease-out translate-y-0 sm:translate-y-2 opacity-100">
        <Card className="relative overflow-hidden p-0 dark:bg-matte-surface shadow-xl border border-matte-border">
          <div className="flex items-center justify-between border-b border-matte-border p-4 bg-matte-base/50">
            <h3 className="font-semibold text-matte-text">Notifications</h3>
            <button
              onClick={onClose}
              className="rounded-full p-1 text-matte-text-muted hover:bg-matte-base hover:text-matte-text transition-colors"
            >
              <X className="h-4 w-4" />
            </button>
          </div>
          
          <div className="max-h-[60vh] overflow-y-auto p-4">
             <div className="flex flex-col gap-4">
                {/* Empty State */}
                <div className="flex flex-col items-center justify-center py-8 text-center">
                  <div className="bg-matte-base p-3 rounded-full mb-3">
                    <span className="text-2xl">🔔</span>
                  </div>
                  <p className="text-sm font-medium text-matte-text">No notifications</p>
                  <p className="text-xs text-matte-text-muted mt-1">We'll notify you when something happens.</p>
                </div>
             </div>
          </div>
          
          <div className="border-t border-matte-border p-3 bg-matte-base/30">
             <button className="w-full text-center text-xs font-medium text-primary hover:text-primary/80 transition-colors">
                Mark all as read
             </button>
          </div>
        </Card>
      </div>
      
      {/* Backdrop for mobile */}
      <div 
        className="fixed inset-0 bg-black/20 backdrop-blur-sm -z-10 sm:hidden" 
        onClick={onClose}
      />
    </div>
  );
}
