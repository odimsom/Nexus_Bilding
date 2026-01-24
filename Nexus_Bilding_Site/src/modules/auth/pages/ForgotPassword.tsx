import { useState, FormEvent } from 'react';
import { Link } from 'react-router-dom';
import { Database, Mail, ArrowLeft, LockKeyhole } from 'lucide-react';

export function ForgotPassword() {
  const [email, setEmail] = useState('');
  const [submitted, setSubmitted] = useState(false);

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    setSubmitted(true);
  };

  return (
    <div className="flex min-h-screen w-full items-center justify-center bg-matte-base p-4 text-matte-text">
      <div className="flex w-full max-w-[420px] flex-col items-center gap-8">
        <div className="flex items-center gap-3">
          <div className="flex size-12 items-center justify-center rounded-xl bg-primary/20 text-primary shadow-sm border border-primary/10">
            <Database className="w-7 h-7" />
          </div>
          <div className="flex flex-col">
            <h1 className="text-xl font-bold tracking-tight text-matte-text">Nexus Billing</h1>
            <p className="text-xs text-matte-text-muted">Secure Access</p>
          </div>
        </div>

        <div className="w-full overflow-hidden rounded-2xl border border-matte-border bg-matte-surface shadow-md">
          <div className="p-8">
            <div className="mb-6 text-center">
              <div className="mb-4 inline-flex h-12 w-12 items-center justify-center rounded-full bg-matte-base text-primary shadow-inner">
                <LockKeyhole className="w-6 h-6" />
              </div>
              <h2 className="text-2xl font-bold text-matte-text">Forgot password?</h2>
              <p className="mt-2 text-sm leading-relaxed text-matte-text-muted">
                {submitted
                  ? 'Check your email for reset instructions.'
                  : "No worries, we'll send you reset instructions."}
              </p>
            </div>

            {!submitted ? (
              <form className="flex flex-col gap-5" onSubmit={handleSubmit}>
                <div className="flex flex-col gap-1.5">
                  <label className="text-sm font-medium text-matte-text" htmlFor="email">
                    Email address
                  </label>
                  <div className="relative">
                    <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3 text-matte-text-muted">
                      <Mail className="w-5 h-5" />
                    </div>
                    <input
                      className="block w-full rounded-lg border border-matte-border bg-matte-input py-2.5 pl-10 pr-3 text-sm text-matte-text placeholder-matte-text-muted/60 focus:border-primary focus:ring-1 focus:ring-primary focus:outline-none transition-all"
                      id="email"
                      placeholder="Enter your email"
                      type="email"
                      required
                      value={email}
                      onChange={(e) => setEmail(e.target.value)}
                    />
                  </div>
                </div>

                <button
                  className="flex w-full items-center justify-center rounded-lg bg-primary py-2.5 text-sm font-semibold text-white shadow-sm hover:bg-primary/90 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-primary transition-colors"
                  type="submit"
                >
                  Send Reset Link
                </button>
              </form>
            ) : (
              <div className="text-center">
                <Link
                  className="inline-flex items-center gap-2 text-sm font-medium text-primary hover:text-primary/80 transition-colors"
                  to="/login"
                >
                  <ArrowLeft className="w-4 h-4" />
                  Back to login
                </Link>
              </div>
            )}
          </div>

          {!submitted && (
            <div className="border-t border-matte-border bg-matte-base/30 px-8 py-4 text-center">
              <Link
                className="inline-flex items-center gap-2 text-sm font-medium text-matte-text-muted hover:text-primary transition-colors"
                to="/login"
              >
                <ArrowLeft className="w-4 h-4" />
                Back to log in
              </Link>
            </div>
          )}
        </div>

        <p className="text-xs text-matte-text-muted opacity-80">
          &copy; 2024 Nexus Systems. Need help?{' '}
          <a className="font-medium hover:text-primary underline decoration-primary/30 underline-offset-2" href="#">
            Contact Support
          </a>
        </p>
      </div>
    </div>
  );
}
