import { useEffect, useState } from 'react';
import { useSearchParams, Link } from 'react-router-dom';
import { CheckCircle2, XCircle, Loader2 } from 'lucide-react';
import { authService } from '../../../services/auth.service';

export function ConfirmEmail() {
  const [searchParams] = useSearchParams();
  const [status, setStatus] = useState<'loading' | 'success' | 'error'>('loading');
  const [message, setMessage] = useState('Verifying your email...');

  useEffect(() => {
    const confirm = async () => {
      const userId = searchParams.get('userId');
      const code = searchParams.get('code');

      if (!userId || !code) {
        setStatus('error');
        setMessage('Invalid confirmation link.');
        return;
      }

      try {
        const response = await authService.confirmEmail(userId, code);
        if (response.succeeded) {
          setStatus('success');
          setMessage('Email confirmed successfully! You can now log in.');
        } else {
          setStatus('error');
          setMessage(response.message || 'Failed to confirm email.');
        }
      } catch (error) {
        setStatus('error');
        setMessage('An error occurred while confirming your email.');
      }
    };

    confirm();
  }, [searchParams]);

  return (
    <div className="flex min-h-screen flex-col items-center justify-center bg-matte-base p-4 text-center">
      <div className="w-full max-w-md rounded-lg border border-matte-border bg-matte-surface p-8 shadow-sm">
        <div className="mb-6 flex justify-center">
          {status === 'loading' && <Loader2 className="h-16 w-16 animate-spin text-primary" />}
          {status === 'success' && <CheckCircle2 className="h-16 w-16 text-ink-paid-text" />}
          {status === 'error' && <XCircle className="h-16 w-16 text-ink-error-text" />}
        </div>
        
        <h2 className="mb-2 text-2xl font-bold text-matte-text">
          {status === 'loading' ? 'Verifying...' : status === 'success' ? 'Email Confirmed' : 'Verification Failed'}
        </h2>
        
        <p className="mb-8 text-matte-text-muted">{message}</p>
        
        {status !== 'loading' && (
          <Link
            to="/login"
            className="inline-flex w-full items-center justify-center rounded-lg bg-primary px-4 py-2.5 text-sm font-bold text-white transition-colors hover:bg-primary/90"
          >
            Back to Login
          </Link>
        )}
      </div>
    </div>
  );
}
