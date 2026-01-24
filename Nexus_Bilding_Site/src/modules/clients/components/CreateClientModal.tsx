import { useState } from 'react';
import { PaginatedModal } from '../../core/components/ui/PaginatedModal';
import { User, Mail, Phone, MapPin, FileText } from 'lucide-react';

interface CreateClientModalProps {
  isOpen: boolean;
  onClose: () => void;
}

export function CreateClientModal({ isOpen, onClose }: CreateClientModalProps) {
  const [formData, setFormData] = useState({
    name: '',
    isCompany: false,
    email: '',
    phone: '',
    address: '',
    city: '',
    taxId: '',
    notes: ''
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };
  
  const handleCheckboxChange = (e: React.ChangeEvent<HTMLInputElement>) => {
     setFormData(prev => ({ ...prev, [e.target.name]: e.target.checked }));
  };

  const steps = [
    {
      id: 'basic',
      title: 'Basic Info',
      component: (
        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-matte-text mb-1">Client Name</label>
            <div className="relative">
               <User className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
               <input
                 name="name"
                 value={formData.name}
                 onChange={handleChange}
                 placeholder="John Doe or Company Inc."
                 className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
               />
            </div>
          </div>
          <div className="flex items-center gap-2 mt-4">
             <input 
               type="checkbox" 
               name="isCompany" 
               checked={formData.isCompany} 
               onChange={handleCheckboxChange}
               id="isCompany"
               className="rounded border-matte-border bg-matte-input text-primary focus:ring-0"
             />
             <label htmlFor="isCompany" className="text-sm text-matte-text">This is a business/company</label>
          </div>
        </div>
      )
    },
    {
      id: 'contact',
      title: 'Contact Details',
      component: (
        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-matte-text mb-1">Email Address</label>
            <div className="relative">
               <Mail className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
               <input
                 type="email"
                 name="email"
                 value={formData.email}
                 onChange={handleChange}
                 placeholder="client@example.com"
                 className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
               />
            </div>
          </div>
          <div>
            <label className="block text-sm font-medium text-matte-text mb-1">Phone Number</label>
            <div className="relative">
               <Phone className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
               <input
                 type="tel"
                 name="phone"
                 value={formData.phone}
                 onChange={handleChange}
                 placeholder="+1 (809) 000-0000"
                 className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
               />
            </div>
          </div>
        </div>
      )
    },
    {
      id: 'fiscal',
      title: 'Fiscal Data',
      component: (
        <div className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
             <div>
                <label className="block text-sm font-medium text-matte-text mb-1">Tax ID / RNC</label>
                <div className="relative">
                   <FileText className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
                   <input
                     name="taxId"
                     value={formData.taxId}
                     onChange={handleChange}
                     placeholder="001-0000000-0"
                     className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
                   />
                </div>
             </div>
             <div>
                <label className="block text-sm font-medium text-matte-text mb-1">City</label>
                <input
                   name="city"
                   value={formData.city}
                   onChange={handleChange}
                   placeholder="Santo Domingo"
                   className="w-full px-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
                 />
             </div>
          </div>
          <div>
            <label className="block text-sm font-medium text-matte-text mb-1">Address</label>
            <div className="relative">
               <MapPin className="absolute left-3 top-3 w-4 h-4 text-matte-text-muted" />
               <textarea
                 name="address"
                 value={formData.address}
                 onChange={handleChange}
                 placeholder="Full street address..."
                 rows={3}
                 className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0 resize-none"
               />
            </div>
          </div>
        </div>
      )
    }
  ];

  const handleComplete = async () => {
    // Simulate API call
    console.log('Creating client:', formData);
    await new Promise(resolve => setTimeout(resolve, 1000));
    onClose();
  };

  return (
    <PaginatedModal
      isOpen={isOpen}
      onClose={onClose}
      title="Add New Client"
      steps={steps}
      onComplete={handleComplete}
    />
  );
}
