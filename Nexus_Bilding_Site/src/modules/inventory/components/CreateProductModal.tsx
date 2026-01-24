import { useState } from 'react';
import { PaginatedModal } from '../../core/components/ui/PaginatedModal';
import { Package, Barcode, DollarSign, Archive, Tag } from 'lucide-react';

interface CreateProductModalProps {
  isOpen: boolean;
  onClose: () => void;
}

export function CreateProductModal({ isOpen, onClose }: CreateProductModalProps) {
  const [formData, setFormData] = useState({
    name: '',
    sku: '',
    description: '',
    price: '',
    cost: '',
    taxRate: '18',
    stock: '',
    minStock: '',
    category: 'General'
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const steps = [
    {
      id: 'overview',
      title: 'Product Overview',
      component: (
        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-matte-text mb-1">Product Name</label>
            <div className="relative">
               <Package className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
               <input
                 name="name"
                 value={formData.name}
                 onChange={handleChange}
                 placeholder="e.g. Ergonomic Chair"
                 className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
               />
            </div>
          </div>
          <div>
            <label className="block text-sm font-medium text-matte-text mb-1">Description</label>
            <textarea
               name="description"
               value={formData.description}
               onChange={handleChange}
               placeholder="Product description..."
               rows={3}
               className="w-full px-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0 resize-none"
             />
          </div>
           <div>
            <label className="block text-sm font-medium text-matte-text mb-1">SKU / Code</label>
            <div className="relative">
               <Barcode className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
               <input
                 name="sku"
                 value={formData.sku}
                 onChange={handleChange}
                 placeholder="PROD-001"
                 className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
               />
            </div>
          </div>
        </div>
      )
    },
    {
      id: 'pricing',
      title: 'Pricing & Cost',
      component: (
        <div className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
             <div>
                <label className="block text-sm font-medium text-matte-text mb-1">Sale Price</label>
                <div className="relative">
                   <DollarSign className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
                   <input
                     name="price"
                     type="number"
                     value={formData.price}
                     onChange={handleChange}
                     placeholder="0.00"
                     className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
                   />
                </div>
             </div>
             <div>
                <label className="block text-sm font-medium text-matte-text mb-1">Cost Price</label>
                <div className="relative">
                   <DollarSign className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
                   <input
                     name="cost"
                     type="number"
                     value={formData.cost}
                     onChange={handleChange}
                     placeholder="0.00"
                     className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
                   />
                </div>
             </div>
          </div>
           <div>
            <label className="block text-sm font-medium text-matte-text mb-1">Tax Rate (%)</label>
            <select
               name="taxRate"
               value={formData.taxRate}
               onChange={handleChange}
               className="w-full px-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
             >
               <option value="18">18% (ITBIS)</option>
               <option value="16">16%</option>
               <option value="0">Exempt</option>
             </select>
          </div>
        </div>
      )
    },
    {
      id: 'inventory',
      title: 'Inventory',
      component: (
        <div className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
             <div>
                <label className="block text-sm font-medium text-matte-text mb-1">Initial Stock</label>
                <div className="relative">
                   <Archive className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
                   <input
                     name="stock"
                     type="number"
                     value={formData.stock}
                     onChange={handleChange}
                     placeholder="0"
                     className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
                   />
                </div>
             </div>
             <div>
                <label className="block text-sm font-medium text-matte-text mb-1">Min. Stock Alert</label>
                <input
                     name="minStock"
                     type="number"
                     value={formData.minStock}
                     onChange={handleChange}
                     placeholder="5"
                     className="w-full px-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
                   />
             </div>
          </div>
          <div>
            <label className="block text-sm font-medium text-matte-text mb-1">Category</label>
            <div className="relative">
               <Tag className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
               <select
                  name="category"
                  value={formData.category}
                  onChange={handleChange}
                  className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
               >
                  <option value="General">General</option>
                  <option value="Electronics">Electronics</option>
                  <option value="Furniture">Furniture</option>
                  <option value="Services">Services</option>
               </select>
            </div>
          </div>
        </div>
      )
    }
  ];

  const handleComplete = async () => {
    // Simulate API call
    console.log('Creating product:', formData);
    await new Promise(resolve => setTimeout(resolve, 1000));
    onClose();
  };

  return (
    <PaginatedModal
      isOpen={isOpen}
      onClose={onClose}
      title="Add New Product"
      steps={steps}
      onComplete={handleComplete}
    />
  );
}
