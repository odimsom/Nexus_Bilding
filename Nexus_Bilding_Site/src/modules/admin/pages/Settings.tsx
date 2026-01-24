import { useState } from 'react';
import { Settings as SettingsIcon, User, Building2, Bell, Shield, Save } from 'lucide-react';
import { DashboardLayout } from '../../core/layouts/DashboardLayout';
import { Card } from '../../core/components/ui/Card';
import { mockUser } from '../../../services/mockData';

export function Settings() {
  const [activeTab, setActiveTab] = useState<'profile' | 'company' | 'notifications' | 'security'>('profile');
  
  // Form states
  const [profileData, setProfileData] = useState({
    name: 'Admin User',
    email: 'admin@nexusbilling.com',
    phone: '+1 (555) 123-4567',
    role: 'Administrator',
  });

  const [companyData, setCompanyData] = useState({
    companyName: 'Nexus Billing Systems',
    taxId: '123-45-6789',
    address: '123 Business Ave',
    city: 'New York',
    zipCode: '10001',
    country: 'United States',
  });

  const [notifications, setNotifications] = useState({
    emailInvoices: true,
    emailPayments: true,
    emailReports: false,
    pushNotifications: true,
  });

  const handleSave = () => {
    console.log('Settings saved!');
  };

  return (
    <DashboardLayout title="Settings" user={mockUser}>
      <div className="mx-auto flex max-w-[1200px] flex-col gap-8">
        {/* Header Section */}
        <div className="flex items-center gap-2.5">
          <SettingsIcon className="w-10 h-10 text-primary" />
          <h1 className="text-2xl font-bold text-matte-text">Settings</h1>
        </div>

        {/* Tabs */}
        <div className="flex gap-2 border-b border-matte-border">
          <button
            className={`px-4 py-2 text-sm font-medium transition-colors border-b-2 ${
              activeTab === 'profile'
                ? 'border-primary text-primary'
                : 'border-transparent text-matte-text-muted hover:text-matte-text'
            }`}
            onClick={() => setActiveTab('profile')}
          >
            <div className="flex items-center gap-2">
              <User className="w-4 h-4" />
              Profile
            </div>
          </button>
          <button
            className={`px-4 py-2 text-sm font-medium transition-colors border-b-2 ${
              activeTab === 'company'
                ? 'border-primary text-primary'
                : 'border-transparent text-matte-text-muted hover:text-matte-text'
            }`}
            onClick={() => setActiveTab('company')}
          >
            <div className="flex items-center gap-2">
              <Building2 className="w-4 h-4" />
              Company
            </div>
          </button>
          <button
            className={`px-4 py-2 text-sm font-medium transition-colors border-b-2 ${
              activeTab === 'notifications'
                ? 'border-primary text-primary'
                : 'border-transparent text-matte-text-muted hover:text-matte-text'
            }`}
            onClick={() => setActiveTab('notifications')}
          >
            <div className="flex items-center gap-2">
              <Bell className="w-4 h-4" />
              Notifications
            </div>
          </button>
          <button
            className={`px-4 py-2 text-sm font-medium transition-colors border-b-2 ${
              activeTab === 'security'
                ? 'border-primary text-primary'
                : 'border-transparent text-matte-text-muted hover:text-matte-text'
            }`}
            onClick={() => setActiveTab('security')}
          >
            <div className="flex items-center gap-2">
              <Shield className="w-4 h-4" />
              Security
            </div>
          </button>
        </div>

        {/* Profile Tab */}
        {activeTab === 'profile' && (
          <Card>
            <h3 className="text-lg font-bold text-matte-text mb-6">Profile Information</h3>
            <div className="space-y-4">
              <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
                <div>
                  <label className="block text-sm font-medium text-matte-text mb-2">
                    Full Name
                  </label>
                  <input
                    className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    type="text"
                    value={profileData.name}
                    onChange={(e) => setProfileData({ ...profileData, name: e.target.value })}
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-matte-text mb-2">
                    Email Address
                  </label>
                  <input
                    className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    type="email"
                    value={profileData.email}
                    onChange={(e) => setProfileData({ ...profileData, email: e.target.value })}
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-matte-text mb-2">
                    Phone Number
                  </label>
                  <input
                    className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    type="tel"
                    value={profileData.phone}
                    onChange={(e) => setProfileData({ ...profileData, phone: e.target.value })}
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-matte-text mb-2">
                    Role
                  </label>
                  <input
                    className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    type="text"
                    value={profileData.role}
                    disabled
                  />
                </div>
              </div>
              <button
                className="flex items-center gap-2 rounded-lg bg-primary px-4 py-2.5 text-sm font-medium text-white hover:bg-primary/90 transition-colors shadow-sm"
                onClick={handleSave}
              >
                <Save className="w-4 h-4" /> Save Changes
              </button>
            </div>
          </Card>
        )}

        {/* Company Tab */}
        {activeTab === 'company' && (
          <Card>
            <h3 className="text-lg font-bold text-matte-text mb-6">Company Information</h3>
            <div className="space-y-4">
              <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
                <div>
                  <label className="block text-sm font-medium text-matte-text mb-2">
                    Company Name
                  </label>
                  <input
                    className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    type="text"
                    value={companyData.companyName}
                    onChange={(e) => setCompanyData({ ...companyData, companyName: e.target.value })}
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-matte-text mb-2">
                    Tax ID / Business Number
                  </label>
                  <input
                    className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    type="text"
                    value={companyData.taxId}
                    onChange={(e) => setCompanyData({ ...companyData, taxId: e.target.value })}
                  />
                </div>
                <div className="sm:col-span-2">
                  <label className="block text-sm font-medium text-matte-text mb-2">
                    Address
                  </label>
                  <input
                    className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    type="text"
                    value={companyData.address}
                    onChange={(e) => setCompanyData({ ...companyData, address: e.target.value })}
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-matte-text mb-2">
                    City
                  </label>
                  <input
                    className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    type="text"
                    value={companyData.city}
                    onChange={(e) => setCompanyData({ ...companyData, city: e.target.value })}
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-matte-text mb-2">
                    ZIP / Postal Code
                  </label>
                  <input
                    className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    type="text"
                    value={companyData.zipCode}
                    onChange={(e) => setCompanyData({ ...companyData, zipCode: e.target.value })}
                  />
                </div>
                <div className="sm:col-span-2">
                  <label className="block text-sm font-medium text-matte-text mb-2">
                    Country
                  </label>
                  <input
                    className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                    type="text"
                    value={companyData.country}
                    onChange={(e) => setCompanyData({ ...companyData, country: e.target.value })}
                  />
                </div>
              </div>
              <button
                className="flex items-center gap-2 rounded-lg bg-primary px-4 py-2.5 text-sm font-medium text-white hover:bg-primary/90 transition-colors shadow-sm"
                onClick={handleSave}
              >
                <Save className="w-4 h-4" /> Save Changes
              </button>
            </div>
          </Card>
        )}

        {/* Notifications Tab */}
        {activeTab === 'notifications' && (
          <Card>
            <h3 className="text-lg font-bold text-matte-text mb-6">Notification Preferences</h3>
            <div className="space-y-4">
              <div className="flex items-center justify-between py-3 border-b border-matte-border/50 last:border-0">
                <div>
                  <p className="text-sm font-medium text-matte-text">Email Invoices</p>
                  <p className="text-xs text-matte-text-muted">Receive notifications when new invoices are created</p>
                </div>
                <input
                  className="h-5 w-5 rounded border-matte-border text-primary focus:ring-0 bg-matte-input cursor-pointer"
                  type="checkbox"
                  checked={notifications.emailInvoices}
                  onChange={(e) => setNotifications({ ...notifications, emailInvoices: e.target.checked })}
                />
              </div>
              <div className="flex items-center justify-between py-3 border-b border-matte-border/50 last:border-0">
                <div>
                  <p className="text-sm font-medium text-matte-text">Email Payments</p>
                  <p className="text-xs text-matte-text-muted">Get notified about payment confirmations</p>
                </div>
                <input
                  className="h-5 w-5 rounded border-matte-border text-primary focus:ring-0 bg-matte-input cursor-pointer"
                  type="checkbox"
                  checked={notifications.emailPayments}
                  onChange={(e) => setNotifications({ ...notifications, emailPayments: e.target.checked })}
                />
              </div>
              <div className="flex items-center justify-between py-3 border-b border-matte-border/50 last:border-0">
                <div>
                  <p className="text-sm font-medium text-matte-text">Weekly Reports</p>
                  <p className="text-xs text-matte-text-muted">Receive weekly business reports via email</p>
                </div>
                <input
                  className="h-5 w-5 rounded border-matte-border text-primary focus:ring-0 bg-matte-input cursor-pointer"
                  type="checkbox"
                  checked={notifications.emailReports}
                  onChange={(e) => setNotifications({ ...notifications, emailReports: e.target.checked })}
                />
              </div>
              <div className="flex items-center justify-between py-3">
                <div>
                  <p className="text-sm font-medium text-matte-text">Push Notifications</p>
                  <p className="text-xs text-matte-text-muted">Enable browser push notifications</p>
                </div>
                <input
                  className="h-5 w-5 rounded border-matte-border text-primary focus:ring-0 bg-matte-input cursor-pointer"
                  type="checkbox"
                  checked={notifications.pushNotifications}
                  onChange={(e) => setNotifications({ ...notifications, pushNotifications: e.target.checked })}
                />
              </div>
              <button
                className="flex items-center gap-2 rounded-lg bg-primary px-4 py-2.5 text-sm font-medium text-white hover:bg-primary/90 transition-colors shadow-sm"
                onClick={handleSave}
              >
                <Save className="w-4 h-4" /> Save Preferences
              </button>
            </div>
          </Card>
        )}

        {/* Security Tab */}
        {activeTab === 'security' && (
          <Card>
            <h3 className="text-lg font-bold text-matte-text mb-6">Security Settings</h3>
            <div className="space-y-6">
              <div>
                <h4 className="text-sm font-semibold text-matte-text mb-4">Change Password</h4>
                <div className="space-y-4">
                  <div>
                    <label className="block text-sm font-medium text-matte-text mb-2">
                      Current Password
                    </label>
                    <input
                      className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                      type="password"
                      placeholder="Enter current password"
                    />
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-matte-text mb-2">
                      New Password
                    </label>
                    <input
                      className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                      type="password"
                      placeholder="Enter new password"
                    />
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-matte-text mb-2">
                      Confirm New Password
                    </label>
                    <input
                      className="block w-full rounded-md border border-matte-border py-2.5 px-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
                      type="password"
                      placeholder="Confirm new password"
                    />
                  </div>
                </div>
              </div>
              <button
                className="flex items-center gap-2 rounded-lg bg-primary px-4 py-2.5 text-sm font-medium text-white hover:bg-primary/90 transition-colors shadow-sm"
                onClick={handleSave}
              >
                <Save className="w-4 h-4" /> Update Password
              </button>
            </div>
          </Card>
        )}
      </div>
    </DashboardLayout>
  );
}
