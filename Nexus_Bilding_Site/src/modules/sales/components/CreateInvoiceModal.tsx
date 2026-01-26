
import { useState } from 'react';
import { PaginatedModal } from '../../core/components/ui/PaginatedModal';
import { FileText, Calendar, User, Package, Plus, Trash2 } from 'lucide-react';
import { fiscalService } from '../../../services/fiscal.service';

interface CreateInvoiceModalProps {
  isOpen: boolean;
  onClose: () => void;
}

export function CreateInvoiceModal({ isOpen, onClose }: CreateInvoiceModalProps) {
  const [formData, setFormData] = useState({
    clientId: '',
    date: new Date().toISOString().split('T')[0],
    dueDate: new Date(Date.now() + 30 * 24 * 60 * 60 * 1000).toISOString().split('T')[0],
    ncfType: 'B01',
    notes: '',
    items: [] as { id: string; description: string; qty: number; price: number }[]
  });

  const [newItem, setNewItem] = useState({ description: '', qty: 1, price: 0 });

  const addItem = () => {
    if (!newItem.description) return;
    setFormData(prev => ({
      ...prev,
      items: [...prev.items, { ...newItem, id: Math.random().toString() }]
    }));
    setNewItem({ description: '', qty: 1, price: 0 });
  };

  const removeItem = (id: string) => {
    setFormData(prev => ({
      ...prev,
      items: prev.items.filter(item => item.id !== id)
    }));
  };

  const totalAmount = formData.items.reduce((sum, item) => sum + (item.qty * item.price), 0);

  const steps = [
    {
      id: 'client',
      title: 'Select Client',
      component: (
        <div className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-matte-text mb-1">Client</label>
            <div className="relative">
               <User className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
               <select
                 value={formData.clientId}
                 onChange={e => setFormData({...formData, clientId: e.target.value})}
                 className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
               >
                 <option value="">Select a client...</option>
                 <option value="1">Acme Corp</option>
                 <option value="2">John Doe</option>
               </select>
            </div>
            <p className="text-xs text-matte-text-muted mt-2">Can't find client? <button className="text-primary hover:underline">Create new</button></p>
          </div>
        </div>
      )
    },
    {
      id: 'details',
      title: 'Invoice Details',
      component: (
        <div className="space-y-4">
          <div className="grid grid-cols-2 gap-4">
             <div>
                <label className="block text-sm font-medium text-matte-text mb-1">Issue Date</label>
                <div className="relative">
                   <Calendar className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
                   <input
                     type="date"
                     value={formData.date}
                     onChange={e => setFormData({...formData, date: e.target.value})}
                     className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
                   />
                </div>
             </div>
             <div>
                <label className="block text-sm font-medium text-matte-text mb-1">Due Date</label>
                <div className="relative">
                   <Calendar className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
                   <input
                     type="date"
                     value={formData.dueDate}
                     onChange={e => setFormData({...formData, dueDate: e.target.value})}
                     className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
                   />
                </div>
             </div>
          </div>
          <div>
            <label className="block text-sm font-medium text-matte-text mb-1">NCF Type</label>
            <div className="relative">
               <FileText className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-matte-text-muted" />
               <select
                  value={formData.ncfType}
                  onChange={e => setFormData({...formData, ncfType: e.target.value})}
                  className="w-full pl-9 pr-3 py-2 bg-matte-input border border-matte-border rounded-lg text-sm focus:border-primary focus:ring-0"
               >
                  <option value="B01">Crédito Fiscal (B01)</option>
                  <option value="B02">Consumidor Final (B02)</option>
                  <option value="B14">Regímen Especial (B14)</option>
                  <option value="B15">Gubernamental (B15)</option>
               </select>
            </div>
          </div>
        </div>
      )
    },
    {
      id: 'items',
      title: 'Add Items',
      component: (
        <div className="space-y-4">
          <div className="bg-matte-base p-4 rounded-lg border border-matte-border">
             <h4 className="text-sm font-medium text-matte-text mb-3">New Item</h4>
             <div className="grid grid-cols-12 gap-2 mb-2">
               <div className="col-span-6">
                 <input 
                   placeholder="Description" 
                   value={newItem.description}
                   onChange={e => setNewItem({...newItem, description: e.target.value})}
                   className="w-full px-2 py-1 text-sm bg-matte-input border border-matte-border rounded"
                 />
               </div>
               <div className="col-span-2">
                 <input 
                   type="number" 
                   placeholder="Qty" 
                   value={newItem.qty}
                   onChange={e => setNewItem({...newItem, qty: parseInt(e.target.value) || 0})}
                   className="w-full px-2 py-1 text-sm bg-matte-input border border-matte-border rounded"
                 />
               </div>
               <div className="col-span-3">
                 <input 
                   type="number" 
                   placeholder="Price" 
                   value={newItem.price}
                   onChange={e => setNewItem({...newItem, price: parseFloat(e.target.value) || 0})}
                   className="w-full px-2 py-1 text-sm bg-matte-input border border-matte-border rounded"
                 />
               </div>
               <div className="col-span-1 flex justify-end">
                 <button onClick={addItem} className="p-1.5 bg-primary text-white rounded hover:bg-primary/90">
                   <Plus className="w-4 h-4" />
                 </button>
               </div>
             </div>
          </div>

          <div className="max-h-40 overflow-y-auto border border-matte-border rounded-lg">
            <table className="w-full text-sm text-left">
              <thead className="bg-matte-base text-matte-text-muted sticky top-0">
                <tr>
                  <th className="px-3 py-2 font-medium">Desc</th>
                  <th className="px-3 py-2 font-medium">Qty</th>
                  <th className="px-3 py-2 font-medium">Total</th>
                  <th className="px-3 py-2"></th>
                </tr>
              </thead>
              <tbody className="divide-y divide-matte-border">
                {formData.items.map(item => (
                  <tr key={item.id}>
                    <td className="px-3 py-2">{item.description}</td>
                    <td className="px-3 py-2">{item.qty}</td>
                    <td className="px-3 py-2">${(item.qty * item.price).toFixed(2)}</td>
                    <td className="px-3 py-2 text-right">
                       <button onClick={() => removeItem(item.id)} className="text-ink-error-text hover:text-red-400">
                         <Trash2 className="w-3 h-3" />
                       </button>
                    </td>
                  </tr>
                ))}
                {formData.items.length === 0 && (
                  <tr>
                    <td colSpan={4} className="px-3 py-4 text-center text-matte-text-muted">No items added</td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
          
          <div className="flex justify-end text-lg font-bold text-matte-text">
            Total: ${totalAmount.toFixed(2)}
          </div>
        </div>
      )
    },
    {
       id: 'review',
       title: 'Review',
       component: (
         <div className="space-y-4">
           <div className="bg-matte-base p-4 rounded-lg border border-matte-border text-sm space-y-2">
             <div className="flex justify-between">
               <span className="text-matte-text-muted">Client:</span>
               <span className="text-matte-text font-medium">{formData.clientId ? 'Selected Client' : 'None'}</span>
             </div>
             <div className="flex justify-between">
               <span className="text-matte-text-muted">Date:</span>
               <span className="text-matte-text font-medium">{formData.date}</span>
             </div>
             <div className="flex justify-between">
               <span className="text-matte-text-muted">Items:</span>
               <span className="text-matte-text font-medium">{formData.items.length}</span>
             </div>
             <div className="pt-2 border-t border-matte-border flex justify-between font-bold text-lg">
               <span className="text-matte-text">Total Amount</span>
               <span className="text-primary">${totalAmount.toFixed(2)}</span>
             </div>
           </div>
           <div className="bg-yellow-500/10 border border-yellow-500/20 p-3 rounded-lg text-yellow-500 text-xs">
             <p>This invoice will be saved as <strong>DRAFT</strong> initially. You can approve it later.</p>
           </div>
         </div>
       )
    }
  ];

  const handleComplete = async () => {
    // Simulate API call
    console.log('Creating invoice:', formData);
    await new Promise(resolve => setTimeout(resolve, 1000));
    onClose();
  };

  return (
    <PaginatedModal
      isOpen={isOpen}
      onClose={onClose}
      title="Create New Invoice"
      steps={steps}
      onComplete={handleComplete}
    />
  );
}
