import { useState, ReactNode } from 'react';
import { X, ChevronLeft, ChevronRight, Check } from 'lucide-react';
import { Card } from '../ui/Card';

interface Step {
  id: string;
  title: string;
  component: ReactNode;
}

interface PaginatedModalProps {
  isOpen: boolean;
  onClose: () => void;
  title: string;
  steps: Step[];
  onComplete: () => void;
  isSubmitting?: boolean;
}

export function PaginatedModal({ isOpen, onClose, title, steps, onComplete, isSubmitting = false }: PaginatedModalProps) {
  const [currentStepIndex, setCurrentStepIndex] = useState(0);

  if (!isOpen) return null;

  const currentStep = steps[currentStepIndex];
  const isFirstStep = currentStepIndex === 0;
  const isLastStep = currentStepIndex === steps.length - 1;

  const handleNext = () => {
    if (!isLastStep) {
      setCurrentStepIndex(prev => prev + 1);
    } else {
      onComplete();
    }
  };

  const handleBack = () => {
    if (!isFirstStep) {
      setCurrentStepIndex(prev => prev - 1);
    }
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 backdrop-blur-sm p-4">
      <Card className="w-full max-w-2xl bg-matte-surface border border-matte-border p-0 overflow-hidden shadow-2xl animate-in fade-in zoom-in duration-200">
        {/* Header */}
        <div className="flex items-center justify-between border-b border-matte-border p-4 bg-matte-base/50">
          <div>
            <h2 className="text-lg font-bold text-matte-text">{title}</h2>
            <p className="text-xs text-matte-text-muted mt-0.5">
              Step {currentStepIndex + 1} of {steps.length}: <span className="font-medium text-primary">{currentStep.title}</span>
            </p>
          </div>
          <button
            onClick={onClose}
            className="rounded-full p-2 text-matte-text-muted hover:bg-matte-base hover:text-matte-text transition-colors"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Progress Bar */}
        <div className="w-full h-1 bg-matte-base">
          <div 
            className="h-full bg-primary transition-all duration-300 ease-out"
            style={{ width: `${((currentStepIndex + 1) / steps.length) * 100}%` }}
          />
        </div>

        {/* Content */}
        <div className="p-6 max-h-[60vh] overflow-y-auto">
          <div className="animate-in slide-in-from-right-4 duration-200" key={currentStep.id}>
            {currentStep.component}
          </div>
        </div>

        {/* Footer */}
        <div className="flex items-center justify-between border-t border-matte-border p-4 bg-matte-base/30">
          <button
            onClick={handleBack}
            disabled={isFirstStep || isSubmitting}
            className={`flex items-center gap-2 px-4 py-2 text-sm font-medium rounded-lg transition-colors ${
              isFirstStep || isSubmitting
                ? 'text-matte-text-muted cursor-not-allowed opacity-50'
                : 'text-matte-text hover:bg-matte-base border border-matte-border'
            }`}
          >
            <ChevronLeft className="w-4 h-4" /> Back
          </button>

          <div className="flex gap-2">
             <button
               onClick={onClose}
               className="px-4 py-2 text-sm font-medium text-matte-text hover:text-matte-text/80"
             >
               Cancel
             </button>
            <button
              onClick={handleNext}
              disabled={isSubmitting}
              className="flex items-center gap-2 px-6 py-2 bg-primary text-white text-sm font-bold rounded-lg hover:bg-primary/90 transition-colors shadow-sm disabled:opacity-70 disabled:cursor-not-allowed"
            >
              {isSubmitting ? (
                'Saving...'
              ) : isLastStep ? (
                <>Complete <Check className="w-4 h-4" /></>
              ) : (
                <>Next <ChevronRight className="w-4 h-4" /></>
              )}
            </button>
          </div>
        </div>
      </Card>
    </div>
  );
}
