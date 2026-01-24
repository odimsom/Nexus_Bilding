import { useRef, useState } from 'react';
import { Camera, Save, Mail, Lock, User as UserIcon } from 'lucide-react';
import { DashboardLayout } from '../../core/layouts/DashboardLayout';
import { Card } from '../../core/components/ui/Card';
import { mockUser } from '../../../services/mockData';
import { uploadProfileImage } from '../../../helpers/UploadImagesProfile';

export function Profile() {
  const [user, setUser] = useState(mockUser);
  const [isEditing, setIsEditing] = useState(false);
  const [isUploading, setIsUploading] = useState(false);
  const fileInputRef = useRef<HTMLInputElement>(null);

  // Form State
  const [formData, setFormData] = useState({
    name: user.name,
    email: user.email,
    currentPassword: '',
    newPassword: '',
    confirmPassword: '',
  });

  const handleImageUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    try {
      setIsUploading(true);
      const newImageUrl = await uploadProfileImage(file);
      // In a real app, we would update the backend here
      setUser(prev => ({ ...prev, avatarUrl: newImageUrl }));
    } catch (error) {
      console.error('Upload failed:', error);
      alert('Failed to upload image');
    } finally {
      setIsUploading(false);
    }
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // Simulate API call
    setTimeout(() => {
      setUser(prev => ({ ...prev, name: formData.name, email: formData.email }));
      setIsEditing(false);
      setFormData(prev => ({ ...prev, currentPassword: '', newPassword: '', confirmPassword: '' }));
      alert('Profile updated successfully!');
    }, 1000);
  };

  return (
    <DashboardLayout title="User Profile" user={user}>
      <div className="mx-auto max-w-4xl space-y-8">
        {/* Profile Header Card */}
        <Card className="relative overflow-hidden border-0 bg-gradient-to-r from-primary/10 to-primary/5 p-8">
          <div className="relative z-10 flex flex-col items-center gap-6 sm:flex-row">
            <div className="group relative">
              <div className="h-24 w-24 overflow-hidden rounded-full border-4 border-white shadow-lg">
                {user.avatarUrl ? (
                  <img src={user.avatarUrl} alt={user.name} className="h-full w-full object-cover" />
                ) : (
                  <div className="flex h-full w-full items-center justify-center bg-matte-base text-2xl font-bold text-matte-text">
                    {user.name.charAt(0)}
                  </div>
                )}
              </div>
              <button
                onClick={() => fileInputRef.current?.click()}
                disabled={isUploading}
                className="absolute bottom-0 right-0 rounded-full bg-primary p-2 text-white shadow-md transition-transform hover:scale-110 disabled:opacity-50"
              >
                <Camera className="h-4 w-4" />
              </button>
              <input
                ref={fileInputRef}
                type="file"
                className="hidden"
                accept="image/*"
                onChange={handleImageUpload}
              />
            </div>
            
            <div className="text-center sm:text-left">
              <h2 className="text-2xl font-bold text-matte-text">{user.name}</h2>
              <p className="text-matte-text-muted">{user.email}</p>
              <div className="mt-2 inline-flex items-center rounded-full bg-primary/10 px-3 py-1 text-xs font-medium text-primary">
                {user.role.toUpperCase()}
              </div>
            </div>

            <div className="ml-auto">
               <button
                  onClick={() => setIsEditing(!isEditing)}
                  className="rounded-lg bg-white px-4 py-2 text-sm font-medium text-matte-text shadow-sm hover:bg-gray-50 border border-matte-border transition-all"
               >
                  {isEditing ? 'Cancel Edit' : 'Edit Profile'}
               </button>
            </div>
          </div>
          
          {/* Decorative background pattern */}
          <div className="absolute right-0 top-0 -mt-16 -mr-16 h-64 w-64 rounded-full bg-primary/5 blur-3xl" />
        </Card>

        {/* Settings Form */}
        <div className="grid gap-8 md:grid-cols-2">
          {/* General Info */}
          <Card>
            <div className="mb-6 flex items-center gap-2 border-b border-matte-border pb-4">
              <UserIcon className="h-5 w-5 text-primary" />
              <h3 className="text-lg font-semibold text-matte-text">General Information</h3>
            </div>
            
            <form id="profile-form" onSubmit={handleSubmit} className="space-y-4">
              <div>
                <label className="mb-1 block text-sm font-medium text-matte-text">Full Name</label>
                <input
                  type="text"
                  disabled={!isEditing}
                  value={formData.name}
                  onChange={e => setFormData({...formData, name: e.target.value})}
                  className="w-full rounded-lg border border-matte-border bg-matte-input px-3 py-2 text-matte-text focus:border-primary focus:ring-1 focus:ring-primary disabled:opacity-60"
                />
              </div>
              
              <div>
                <label className="mb-1 block text-sm font-medium text-matte-text">Email Address</label>
                <div className="relative">
                  <Mail className="absolute left-3 top-2.5 h-4 w-4 text-matte-text-muted" />
                  <input
                    type="email"
                    disabled={!isEditing}
                    value={formData.email}
                    onChange={e => setFormData({...formData, email: e.target.value})}
                    className="w-full rounded-lg border border-matte-border bg-matte-input pl-10 pr-3 py-2 text-matte-text focus:border-primary focus:ring-1 focus:ring-primary disabled:opacity-60"
                  />
                </div>
              </div>
            </form>
          </Card>

          {/* Security */}
          <Card>
            <div className="mb-6 flex items-center gap-2 border-b border-matte-border pb-4">
              <Lock className="h-5 w-5 text-primary" />
              <h3 className="text-lg font-semibold text-matte-text">Security</h3>
            </div>
            
            <div className="space-y-4">
              <div>
                <label className="mb-1 block text-sm font-medium text-matte-text">Current Password</label>
                <input
                  type="password"
                  disabled={!isEditing}
                  placeholder="••••••••"
                  value={formData.currentPassword}
                  onChange={e => setFormData({...formData, currentPassword: e.target.value})}
                  className="w-full rounded-lg border border-matte-border bg-matte-input px-3 py-2 text-matte-text focus:border-primary focus:ring-1 focus:ring-primary disabled:opacity-60"
                />
              </div>
              
              <div>
                <label className="mb-1 block text-sm font-medium text-matte-text">New Password</label>
                <input
                  type="password"
                  disabled={!isEditing}
                  placeholder="Leave blank to keep current"
                  value={formData.newPassword}
                  onChange={e => setFormData({...formData, newPassword: e.target.value})}
                  className="w-full rounded-lg border border-matte-border bg-matte-input px-3 py-2 text-matte-text focus:border-primary focus:ring-1 focus:ring-primary disabled:opacity-60"
                />
              </div>
              
              <div>
                <label className="mb-1 block text-sm font-medium text-matte-text">Confirm New Password</label>
                <input
                  type="password"
                  disabled={!isEditing}
                  placeholder="••••••••"
                  value={formData.confirmPassword}
                  onChange={e => setFormData({...formData, confirmPassword: e.target.value})}
                  className="w-full rounded-lg border border-matte-border bg-matte-input px-3 py-2 text-matte-text focus:border-primary focus:ring-1 focus:ring-primary disabled:opacity-60"
                />
              </div>
            </div>
          </Card>
        </div>

        {isEditing && (
          <div className="flex justify-end gap-3 sticky bottom-4 bg-matte-base/80 backdrop-blur p-4 rounded-xl border border-matte-border shadow-lg animate-in slide-in-from-bottom-4 fade-in">
            <button
              onClick={() => setIsEditing(false)}
              className="rounded-lg border border-matte-border bg-white px-4 py-2 text-sm font-medium text-matte-text hover:bg-gray-50 transition-colors"
            >
              Cancel
            </button>
            <button
              onClick={handleSubmit}
              className="flex items-center gap-2 rounded-lg bg-primary px-4 py-2 text-sm font-medium text-white hover:bg-primary/90 transition-all shadow-md active:scale-95"
            >
              <Save className="h-4 w-4" /> Save Changes
            </button>
          </div>
        )}
      </div>
    </DashboardLayout>
  );
}
