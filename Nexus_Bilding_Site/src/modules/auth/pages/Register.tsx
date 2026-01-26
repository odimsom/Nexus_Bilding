import { useState, FormEvent } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Database, Mail, Lock } from 'lucide-react';
import { authService } from '../../../services/auth.service';

export function Register() {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [name, setName] = useState('');
  const [lastName, setLastName] = useState('');
  const [phone, setPhone] = useState('');
  const [address, setAddress] = useState('');
  const [city, setCity] = useState('');
  const [zipCode, setZipCode] = useState('');
  const [state, setState] = useState('');
  const [IDNumber, SetIDNumber] = useState('');
  const [role, setRole] = useState('');
  const [company, setCompany] = useState('');
  const [rememberMe, setRememberMe] = useState(false);
  const [loading, setLoading] = useState(false);

  const [success, setSuccess] = useState(false);

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    setLoading(true);
    
    try {
        // Using temporary password confirmation logic as frontend field doesn't exist yet or just duplicate it
        const confirmPassword = password; 
        await authService.register(name, email, password, confirmPassword);
        setSuccess(true);
    } catch(err) {
        console.error(err);
        // Handle error (show message)
    } finally {
        setLoading(false);
    }
  };

  if (success) {
      return (
        <div className="bg-matte-base font-display text-matte-text antialiased min-h-screen flex flex-col justify-center py-12 sm:px-6 lg:px-8">
            <div className="sm:mx-auto sm:w-full sm:max-w-md text-center">
                <div className="mx-auto flex h-12 w-12 items-center justify-center rounded-full bg-green-100">
                    <Database className="h-6 w-6 text-green-600" aria-hidden="true" />
                </div>
                <h2 className="mt-6 text-3xl font-bold tracking-tight text-matte-text">Registration Successful</h2>
                <p className="mt-2 text-sm text-matte-text-muted">
                    We have sent an email to <span className="font-semibold text-matte-text">{email}</span> with a link to activate your account.
                </p>
                <div className="mt-6">
                    <Link to="/login" className="font-medium text-primary hover:text-primary/80">
                        Back to Login
                    </Link>
                </div>
            </div>
        </div>
      );
  }

  return (
    <div className="bg-matte-base font-display text-matte-text antialiased min-h-screen flex flex-col justify-center py-12 sm:px-6 lg:px-8">
      <div className="sm:mx-auto sm:w-full sm:max-w-2xl">
        <div className="flex flex-col items-center justify-center mb-8">
          <div className="flex items-center gap-3 text-matte-text mb-4">
            <div className="size-10 text-primary">
              <Database className="w-full h-full" />
            </div>
            <h2 className="text-2xl font-bold leading-tight tracking-tight text-matte-text">Nexus Billing</h2>
          </div>
          <p className="text-matte-text font-medium text-lg">Create your account</p>
        </div>

        <div className="bg-matte-surface py-10 px-8 shadow-none rounded-lg border border-matte-border sm:px-10">
          <form onSubmit={handleSubmit} className="space-y-6">
            {/* Personal Information */}
            <div>
              <h3 className="text-lg font-semibold text-matte-text mb-4">Personal Information</h3>
              <div className="grid grid-cols-1 gap-6 sm:grid-cols-2">
                <div>
                  <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="name">
                    First Name
                  </label>
                  <input
                    autoComplete="given-name"
                    className="block w-full rounded-md border border-matte-border py-3 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    id="name"
                    name="name"
                    placeholder="John"
                    required
                    type="text"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                  />
                </div>

                <div>
                  <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="lastName">
                    Last Name
                  </label>
                  <input
                    autoComplete="family-name"
                    className="block w-full rounded-md border border-matte-border py-3 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    id="lastName"
                    name="lastName"
                    placeholder="Doe"
                    required
                    type="text"
                    value={lastName}
                    onChange={(e) => setLastName(e.target.value)}
                  />
                </div>
              </div>
            </div>

            {/* Account Information */}
            <div>
              <h3 className="text-lg font-semibold text-matte-text mb-4">Account Information</h3>
              <div className="space-y-4">
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
                      autoComplete="new-password"
                      className="block w-full rounded-md border border-matte-border py-3 pl-10 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                      id="password"
                      name="password"
                      placeholder="Create a strong password"
                      required
                      type="password"
                      value={password}
                      onChange={(e) => setPassword(e.target.value)}
                    />
                  </div>
                </div>
              </div>
            </div>

            {/* Company/Client Information */}
            <div>
              <h3 className="text-lg font-semibold text-matte-text mb-4">Company Information</h3>
              <div className="space-y-4">
                <div className="grid grid-cols-1 gap-6 sm:grid-cols-2">
                  <div>
                    <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="company">
                      Company Name
                    </label>
                    <input
                      autoComplete="organization"
                      className="block w-full rounded-md border border-matte-border py-3 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                      id="company"
                      name="company"
                      placeholder="Acme Inc."
                      required
                      type="text"
                      value={company}
                      onChange={(e) => setCompany(e.target.value)}
                    />
                  </div>

                  <div>
                    <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="IDNumber">
                      Tax ID / Business Number
                    </label>
                    <input
                      className="block w-full rounded-md border border-matte-border py-3 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                      id="IDNumber"
                      name="IDNumber"
                      placeholder="123456789"
                      required
                      type="text"
                      value={IDNumber}
                      onChange={(e) => SetIDNumber(e.target.value)}
                    />
                  </div>
                </div>

                <div>
                  <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="phone">
                    Phone Number
                  </label>
                  <input
                    autoComplete="tel"
                    className="block w-full rounded-md border border-matte-border py-3 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    id="phone"
                    name="phone"
                    placeholder="+1 (555) 000-0000"
                    required
                    type="tel"
                    value={phone}
                    onChange={(e) => setPhone(e.target.value)}
                  />
                </div>

                <div>
                  <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="address">
                    Address
                  </label>
                  <input
                    autoComplete="street-address"
                    className="block w-full rounded-md border border-matte-border py-3 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    id="address"
                    name="address"
                    placeholder="123 Main Street"
                    required
                    type="text"
                    value={address}
                    onChange={(e) => setAddress(e.target.value)}
                  />
                </div>

                <div className="grid grid-cols-1 gap-6 sm:grid-cols-3">
                  <div>
                    <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="city">
                      City
                    </label>
                    <input
                      autoComplete="address-level2"
                      className="block w-full rounded-md border border-matte-border py-3 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                      id="city"
                      name="city"
                      placeholder="New York"
                      required
                      type="text"
                      value={city}
                      onChange={(e) => setCity(e.target.value)}
                    />
                  </div>

                  <div>
                    <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="state">
                      State/Province
                    </label>
                    <input
                      autoComplete="address-level1"
                      className="block w-full rounded-md border border-matte-border py-3 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                      id="state"
                      name="state"
                      placeholder="NY"
                      required
                      type="text"
                      value={state}
                      onChange={(e) => setState(e.target.value)}
                    />
                  </div>

                  <div>
                    <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="zipCode">
                      ZIP/Postal Code
                    </label>
                    <input
                      autoComplete="postal-code"
                      className="block w-full rounded-md border border-matte-border py-3 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                      id="zipCode"
                      name="zipCode"
                      placeholder="10001"
                      required
                      type="text"
                      value={zipCode}
                      onChange={(e) => setZipCode(e.target.value)}
                    />
                  </div>
                </div>

                <div>
                  <label className="block text-sm font-medium leading-6 text-matte-text pb-2" htmlFor="role">
                    Your Role
                  </label>
                  <select
                    className="block w-full rounded-md border border-matte-border py-3 px-4 text-matte-text bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    id="role"
                    name="role"
                    required
                    value={role}
                    onChange={(e) => setRole(e.target.value)}
                  >
                    <option value="">Select your role</option>
                    <option value="owner">Owner</option>
                    <option value="manager">Manager</option>
                    <option value="accountant">Accountant</option>
                    <option value="staff">Staff</option>
                  </select>
                </div>
              </div>
            </div>

            {/* Terms and Conditions */}
            <div className="flex items-start">
              <input
                className="h-4 w-4 mt-1 rounded border-matte-border text-primary focus:ring-0 bg-matte-input cursor-pointer"
                id="terms"
                name="terms"
                type="checkbox"
                required
                checked={rememberMe}
                onChange={(e) => setRememberMe(e.target.checked)}
              />
              <label className="ml-2 block text-sm text-matte-text select-none cursor-pointer" htmlFor="terms">
                I agree to the{' '}
                <Link className="font-medium text-primary hover:text-primary/80 transition-colors" to="/terms">
                  Terms of Service
                </Link>{' '}
                and{' '}
                <Link className="font-medium text-primary hover:text-primary/80 transition-colors" to="/privacy">
                  Privacy Policy
                </Link>
              </label>
            </div>

            <div>
              <button
                className="flex w-full justify-center rounded-md bg-primary px-3 py-3 text-sm font-bold leading-6 text-white shadow-none hover:bg-primary/90 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-primary transition-all duration-200"
                type="submit"
                disabled={loading}
              >
                {loading ? 'Creating account...' : 'Create Account'}
              </button>
            </div>
          </form>

          <p className="mt-8 text-center text-sm text-matte-text/80">
            Already have an account?{' '}
            <Link className="font-semibold leading-6 text-primary hover:text-primary/80 transition-colors" to="/login">
              Sign in
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
