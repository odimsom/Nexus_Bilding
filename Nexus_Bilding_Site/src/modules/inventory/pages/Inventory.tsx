import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Plus, Search, Package, MoreHorizontal } from 'lucide-react';
import { DashboardLayout } from '../../core/layouts/DashboardLayout';
import { Badge } from '../../core/components/ui/Badge';
import { LoadingState } from '../../core/components/ui/LoadingState';
import { api } from '../../../services/api';
import { mockUser } from '../../../services/mockData';
import { CreateProductModal } from '../components/CreateProductModal';
import { Product } from '../../../types';

export function Products() {
  const navigate = useNavigate();
  const [products, setProducts] = useState<Product[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState<'all' | 'active' | 'inactive'>('all');
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const data = await api.products.getAll();
        setProducts(data);
      } finally {
        setIsLoading(false);
      }
    };
    fetchProducts();
  }, []);

  // Filter products based on search and status
  const filteredProducts = products.filter((product) => {
    const matchesSearch =
      product.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
      (product.sku && product.sku.toLowerCase().includes(searchTerm.toLowerCase())) ||
      (product.category && product.category.toLowerCase().includes(searchTerm.toLowerCase()));
    const matchesStatus = statusFilter === 'all' || product.status === statusFilter;
    return matchesSearch && matchesStatus;
  });

  // Calculate stock status
  const getStockStatus = (product: Product) => {
    const available = product.stock || 0;
    const threshold = product.lowStockThreshold || 5;
    
    if (available <= 0) return { status: 'out-of-stock', color: 'text-ink-error-text' };
    if (available <= threshold) return { status: 'low-stock', color: 'text-ink-pending-text' };
    return { status: 'in-stock', color: 'text-ink-paid-text' };
  };

  return (
    <DashboardLayout title="Inventory" user={mockUser}>
      <div className="mx-auto flex max-w-[1200px] flex-col gap-8">
        {/* Header Section */}
        <div className="flex flex-col gap-4 sm:flex-row sm:items-center sm:justify-between">
          <div className="flex items-center gap-2.5">
            <Package className="w-10 h-10 text-primary" />
            <span className="bg-matte-base text-matte-text text-sm font-semibold px-2.5 py-1 rounded-full border border-matte-border">
              {filteredProducts.length}
            </span>
          </div>
          <button
            className="flex items-center gap-2 rounded-lg bg-primary px-4 py-2 text-sm font-medium text-white hover:bg-primary/90 transition-all duration-200 ease-out hover:shadow-md active:scale-[0.98]"
            onClick={() => setIsModalOpen(true)}
          >
            <Plus className="w-4 h-4" /> New Product
          </button>
        </div>

        {/* Search and Filters */}
        <div className="flex flex-col gap-4 sm:flex-row sm:items-center">
          <div className="relative flex-1">
            <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <Search className="text-matte-text-muted w-5 h-5" />
            </div>
            <input
              className="block w-full rounded-md border border-matte-border py-2.5 pl-10 pr-4 text-matte-text placeholder:text-matte-text-muted bg-matte-input text-sm leading-6 focus:border-primary focus:ring-0"
              placeholder="Search products by name, SKU or category..."
              type="text"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
          </div>
          <div className="flex gap-2">
            <button
              className={`rounded-lg border px-4 py-2 text-sm font-medium transition-colors ${
                statusFilter === 'all'
                  ? 'bg-primary text-white border-primary'
                  : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
              }`}
              onClick={() => setStatusFilter('all')}
            >
              All
            </button>
            <button
              className={`rounded-lg border px-4 py-2 text-sm font-medium transition-colors ${
                statusFilter === 'active'
                  ? 'bg-primary text-white border-primary'
                  : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
              }`}
              onClick={() => setStatusFilter('active')}
            >
              Active
            </button>
            <button
              className={`rounded-lg border px-4 py-2 text-sm font-medium transition-colors ${
                statusFilter === 'inactive'
                  ? 'bg-primary text-white border-primary'
                  : 'bg-matte-base border-matte-border text-matte-text hover:bg-matte-border/20'
              }`}
              onClick={() => setStatusFilter('inactive')}
            >
              Inactive
            </button>
          </div>
        </div>

        {/* Table Section */}
        <div className="rounded-xl border border-matte-border bg-matte-surface shadow-sm">
          {isLoading ? (
            <LoadingState message="Loading inventory..." />
          ) : (
            <div className="overflow-x-auto">
              <table className="w-full min-w-[900px] text-left">
                <thead>
                  <tr className="border-b border-matte-border bg-matte-base/50 text-sm leading-normal text-matte-text-muted">
                    <th className="px-6 py-3 font-medium">Product</th>
                    <th className="px-6 py-3 font-medium">Category</th>
                    <th className="px-6 py-3 font-medium">Price</th>
                    <th className="px-6 py-3 font-medium">Stock</th>
                    <th className="px-6 py-3 font-medium">Status</th>
                    <th className="px-6 py-3 font-medium text-right">Actions</th>
                  </tr>
                </thead>
                <tbody className="text-sm text-matte-text">
                  {filteredProducts.length > 0 ? (
                    filteredProducts.map((product) => {
                      const available = product.stock || 0;
                      const stockStatus = getStockStatus(product);
                      
                      return (
                        <tr
                          key={product.id}
                          className="border-b border-matte-border last:border-0 hover:bg-matte-base/30 transition-all duration-150 ease-out hover:shadow-sm group cursor-pointer"
                          onClick={() => navigate(`${product.id}`)}
                        >
                          <td className="px-6 py-4">
                            <div className="flex flex-col">
                              <p className="font-medium text-matte-text group-hover:text-primary transition-colors">
                                {product.name}
                              </p>
                              <p className="text-xs text-matte-text-muted font-mono">{product.sku || 'N/A'}</p>
                            </div>
                          </td>
                          <td className="px-6 py-4">
                            <div className="flex flex-col">
                              <span className="text-sm font-medium">{product.category || 'Uncategorized'}</span>
                              <span className="text-xs text-matte-text-muted">ITBIS: {product.itbisRate}%</span>
                            </div>
                          </td>
                          <td className="px-6 py-4">
                            <div className="flex flex-col">
                              <span className="text-sm font-bold">${product.price.toLocaleString()}</span>
                              {product.cost && (
                                <span className="text-xs text-matte-text-muted">Cost: ${product.cost}</span>
                              )}
                            </div>
                          </td>
                          <td className="px-6 py-4">
                            <div className="flex flex-col">
                              <span className={`text-sm font-semibold ${stockStatus.color}`}>
                                {available} available
                              </span>
                            </div>
                          </td>
                          <td className="px-6 py-4">
                            <Badge status={product.status}>{product.status}</Badge>
                          </td>
                          <td className="px-6 py-4 text-right">
                            <button
                              className="text-matte-text-muted hover:text-primary transition-colors"
                              onClick={(e) => {
                                e.stopPropagation();
                                console.log('More actions for', product.name);
                              }}
                            >
                              <MoreHorizontal className="w-5 h-5" />
                            </button>
                          </td>
                        </tr>
                      );
                    })
                  ) : (
                    <tr>
                      <td colSpan={6} className="px-6 py-12 text-center">
                        <div className="flex flex-col items-center gap-3">
                          <Package className="w-12 h-12 text-matte-text-muted/50" />
                          <div>
                            <p className="text-lg font-medium text-matte-text mb-1">No products found</p>
                            <p className="text-sm text-matte-text-muted">
                              {searchTerm || statusFilter !== 'all'
                                ? 'Try adjusting your search or filters'
                                : 'Get started by adding your first product'}
                            </p>
                          </div>
                        </div>
                      </td>
                    </tr>
                  )}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>
      <CreateProductModal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} />
    </DashboardLayout>
  );
}