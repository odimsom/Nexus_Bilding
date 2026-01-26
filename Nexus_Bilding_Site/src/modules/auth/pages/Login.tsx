import { useState, FormEvent } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Database, Mail, Lock } from 'lucide-react';
import { authService } from '../../../services/auth.service';

export function Login() {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [rememberMe, setRememberMe] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      const response = await authService.login(email, password);
      // Backend returns User with Role, navigate accordingly
      const userRole = response.user.roles && response.user.roles.length > 0 ? response.user.roles[0] : 'seller'; // Default fallback
      
      // Simple mapping if backend roles differ from frontend routes (e.g. 'Admin' -> 'admin')
      const routeRole = userRole.toLowerCase();
      
      navigate(`/${routeRole}/dashboard`);
    } catch (err: any) {
      console.error(err);
      setError('Invalid email or password.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="bg-matte-base font-display text-matte-text antialiased min-h-screen flex flex-col justify-center py-12 sm:px-6 lg:px-8">
      <div className="sm:mx-auto sm:w-full sm:max-w-md">
        <div className="flex flex-col items-center justify-center mb-8">
          <div className="flex items-center gap-3 text-matte-text mb-4">
            <div className="size-10 text-primary">
              <Database className="w-full h-full" />
            </div>
            <h2 className="text-2xl font-bold leading-tight tracking-tight text-matte-text">Nexus Billing</h2>
          </div>
          <p className="text-matte-text font-medium text-lg">Sign in to your dashboard</p>
        </div>

        <div className="bg-matte-surface py-10 px-8 shadow-none rounded-lg border border-matte-border sm:px-10">
          <form onSubmit={handleSubmit} className="space-y-6">
            {error && (
              <div className="bg-red-500/10 border border-red-500/20 text-red-500 px-4 py-2 rounded-md text-sm">
                {error}
              </div>
            )}
            <div>
              <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="email">
                Email Address
              </label>
              <div className="relative">
                <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                  <Mail className="text-matte-text-muted w-5 h-5" />
                </div>
                <input
                  autoComplete="email"
                  className="block w-full rounded-md border border-matte-border py-3 pl-10 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                  id="email"
                  name="email"
                  placeholder="name@company.com"
                  required
                  type="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
              </div>
            </div>

            <div>
              <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="password">
                Password
              </label>
              <div className="relative">
                <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                  <Lock className="text-matte-text-muted w-5 h-5" />
                </div>
                <input
                  autoComplete="current-password"
                  className="block w-full rounded-md border border-matte-border py-3 pl-10 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                  id="password"
                  name="password"
                  placeholder="Enter your password"
                  required
                  type="password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
              </div>
            </div>

            <div className="flex items-center justify-between">
              <div className="flex items-center">
                <input
                  className="h-4 w-4 rounded border-matte-border text-primary focus:ring-0 bg-matte-input cursor-pointer"
                  id="remember-me"
                  name="remember-me"
                  type="checkbox"
                  checked={rememberMe}
                  onChange={(e) => setRememberMe(e.target.checked)}
                />
                <label className="ml-2 block text-sm text-matte-text select-none cursor-pointer" htmlFor="remember-me">
                  Remember me
                </label>
              </div>

              <div className="text-sm">
                <Link className="font-medium text-primary hover:text-primary/80 transition-colors" to="/forgot-password">
                  Forgot password?
                </Link>
              </div>
            </div>

            <div>
              <button
                className="flex w-full justify-center rounded-md bg-primary px-3 py-3 text-sm font-bold leading-6 text-white shadow-none hover:bg-primary/90 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-primary transition-all duration-200"
                type="submit"
                disabled={loading}
              >
                {loading ? 'Signing in...' : 'Sign In'}
              </button>
            </div>
          </form>

          <p className="mt-8 text-center text-sm text-matte-text/80">
            Don't have an account?{' '}
            <Link className="font-semibold leading-6 text-primary hover:text-primary/80 transition-colors" to="/register">
              Sign up
            </Link>
          </p>
        </div>

        <div className="mt-8 text-center text-xs text-matte-text-muted">
          <p>&copy; 2024 Nexus Billing Systems. All rights reserved.</p>
        </div>
      </div>
    </div>
  );
}
