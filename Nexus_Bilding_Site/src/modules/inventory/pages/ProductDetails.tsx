import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { ShoppingCart, TrendingUp, Truck, RotateCcw, Filter, Download, Image as ImageIcon, Plus, Minus } from 'lucide-react';
import { DashboardLayout } from '../../core/layouts/DashboardLayout';
import { Card } from '../../core/components/ui/Card';
import { Badge } from '../../core/components/ui/Badge';
import { api } from '../../../services/api';
import { mockUser } from '../../../services/mockData';
import { Product, StockTransaction } from '../../../types';

export function ProductDetails() {
  const { id } = useParams();
  const [product, setProduct] = useState<Product | null>(null);
  const [transactions, setTransactions] = useState<StockTransaction[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      if (id) {
        const [productData, transactionsData] = await Promise.all([
          api.products.getById(id),
          api.products.getStockTransactions(id),
        ]);
        if (productData) setProduct(productData);
        setTransactions(transactionsData);
      }
    };
    fetchData();
  }, [id]);

  if (!product) {
    return (
      <DashboardLayout title="Product Details" user={mockUser}>
        <div className="flex h-64 items-center justify-center">
          <div className="text-matte-text-muted">Loading...</div>
        </div>
      </DashboardLayout>
    );
  }

  const threshold = product.lowStockThreshold || 5;
  const stock = product.stock || 0;
  const stockPercentage = Math.min((stock / (threshold * 6)) * 100, 100);

  return (
    <DashboardLayout title="Product Details" user={mockUser}>
      <div className="mx-auto flex max-w-[1200px] flex-col gap-8">
        <div className="grid grid-cols-1 gap-8 lg:grid-cols-12">
          <div className="flex flex-col gap-6 lg:col-span-4">
            <Card>
              <div className="mb-6 aspect-square w-full overflow-hidden rounded-lg border border-matte-border bg-matte-base/50">
                <div className="flex h-full w-full items-center justify-center bg-gray-100">
                  <ImageIcon className="w-16 h-16 text-matte-border" />
                </div>
              </div>

              <div className="mb-2 flex items-start justify-between">
                <h3 className="text-xl font-bold text-matte-text">{product.name}</h3>
                <Badge status={product.status}>{product.status}</Badge>
              </div>

              <p className="mb-6 text-sm text-matte-text-muted">{product.description}</p>

              <div className="space-y-4">
                <div className="flex justify-between border-b border-matte-border/50 pb-2">
                  <span className="text-sm font-medium text-matte-text-muted">SKU</span>
                  <span className="text-sm font-semibold text-matte-text">{product.sku || 'N/A'}</span>
                </div>
                <div className="flex justify-between border-b border-matte-border/50 pb-2">
                  <span className="text-sm font-medium text-matte-text-muted">Category</span>
                  <span className="text-sm font-semibold text-matte-text">{product.category || 'N/A'}</span>
                </div>
                <div className="flex justify-between border-b border-matte-border/50 pb-2">
                  <span className="text-sm font-medium text-matte-text-muted">ITBIS Rate</span>
                  <span className="text-sm font-semibold text-matte-text">{product.itbisRate}%</span>
                </div>
                <div className="flex justify-between pb-2">
                  <span className="text-sm font-medium text-matte-text-muted">Unit Price</span>
                  <span className="text-sm font-semibold text-matte-text">${product.price.toLocaleString()}</span>
                </div>
                {product.cost && (
                  <div className="flex justify-between pb-2">
                    <span className="text-sm font-medium text-matte-text-muted">Cost</span>
                    <span className="text-sm font-semibold text-matte-text">${product.cost.toLocaleString()}</span>
                  </div>
                )}
              </div>
            </Card>

            <Card>
              <h4 className="mb-4 text-sm font-bold uppercase tracking-wider text-matte-text-muted">Inventory Status</h4>
              <div className="mb-6 flex items-center justify-between">
                <div>
                  <p className="text-3xl font-bold text-matte-text">{stock}</p>
                  <p className="text-xs text-matte-text-muted">Units in Stock</p>
                </div>
              </div>

              <div className="w-full rounded-full bg-matte-base h-2.5 mb-2 border border-matte-border/30">
                <div className={`h-2 rounded-full ${stock <= threshold ? 'bg-ink-error-text' : 'bg-primary'}`} style={{ width: `${stockPercentage}%` }} />
              </div>
              <div className="flex justify-between text-xs text-matte-text-muted">
                <span>Low stock alert at {threshold} units</span>
                <span>Optimal: {threshold * 6}</span>
              </div>
            </Card>
          </div>

          <div className="flex flex-col gap-6 lg:col-span-8">
            <div className="grid grid-cols-1 gap-4 sm:grid-cols-3">
              <Card className="p-4">
                <div className="flex items-center gap-3">
                  <div className="flex size-10 items-center justify-center rounded-lg bg-matte-base text-matte-text-muted border border-matte-border/50">
                    <TrendingUp className="w-5 h-5" />
                  </div>
                  <div>
                    <p className="text-sm font-medium text-matte-text-muted">Total Sold</p>
                    <p className="text-lg font-bold text-matte-text">0</p>
                  </div>
                </div>
              </Card>

              <Card className="p-4">
                <div className="flex items-center gap-3">
                  <div className="flex size-10 items-center justify-center rounded-lg bg-matte-base text-matte-text-muted border border-matte-border/50">
                    <Truck className="w-5 h-5" />
                  </div>
                  <div>
                    <p className="text-sm font-medium text-matte-text-muted">In Transit</p>
                    <p className="text-lg font-bold text-matte-text">0</p>
                  </div>
                </div>
              </Card>

              <Card className="p-4">
                <div className="flex items-center gap-3">
                  <div className="flex size-10 items-center justify-center rounded-lg bg-matte-base text-matte-text-muted border border-matte-border/50">
                    <RotateCcw className="w-5 h-5" />
                  </div>
                  <div>
                    <p className="text-sm font-medium text-matte-text-muted">Returns</p>
                    <p className="text-lg font-bold text-matte-text">0</p>
                  </div>
                </div>
              </Card>
            </div>

            <div className="flex flex-col rounded-xl border border-matte-border bg-matte-surface shadow-sm flex-1">
              <div className="flex items-center justify-between border-b border-matte-border px-6 py-4">
                <h3 className="text-lg font-bold text-matte-text">Stock History</h3>
                <div className="flex items-center gap-2">
                  <button className="flex items-center gap-1 text-sm font-medium text-matte-text-muted hover:text-matte-text transition-colors">
                    <Filter className="w-4 h-4" />
                    Filter
                  </button>
                  <button className="flex items-center gap-1 text-sm font-medium text-matte-text-muted hover:text-matte-text transition-colors">
                    <Download className="w-4 h-4" />
                    Export
                  </button>
                </div>
              </div>

              <div className="overflow-x-auto">
                <table className="w-full min-w-[600px] text-left">
                  <thead>
                    <tr className="border-b border-matte-border bg-matte-base/50 text-sm leading-normal text-matte-text-muted">
                      <th className="px-6 py-3 font-medium">Date</th>
                      <th className="px-6 py-3 font-medium">Type</th>
                      <th className="px-6 py-3 font-medium">Reference</th>
                      <th className="px-6 py-3 font-medium text-right">Quantity</th>
                      <th className="px-6 py-3 font-medium text-right">Balance</th>
                    </tr>
                  </thead>
                  <tbody className="text-sm text-matte-text">
                    {transactions.length > 0 ? (
                      transactions.map((transaction) => (
                        <tr
                          key={transaction.id}
                          className="border-b border-matte-border last:border-0 hover:bg-matte-base/30 transition-colors"
                        >
                          <td className="px-6 py-4">{new Date(transaction.createdAt).toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' })}</td>
                          <td className="px-6 py-4">
                            <div className="flex items-center gap-2">
                              {transaction.type === 'purchase' ? (
                                <Plus className="w-4 h-4 text-ink-paid-text" />
                              ) : transaction.type === 'sale' ? (
                                <ShoppingCart className="w-4 h-4 text-matte-text-muted" />
                              ) : (
                                <Minus className="w-4 h-4 text-ink-pending-text" />
                              )}
                              <span className="capitalize">{transaction.type}</span>
                            </div>
                          </td>
                          <td className="px-6 py-4 text-matte-text-muted">{transaction.reference || '-'}</td>
                          <td className={`px-6 py-4 text-right font-medium ${transaction.quantity > 0 ? 'text-ink-paid-text' : 'text-matte-text'}`}>
                            {transaction.quantity > 0 ? '+' : ''}{transaction.quantity}
                          </td>
                          <td className="px-6 py-4 text-right">{transaction.balanceAfter}</td>
                        </tr>
                      ))
                    ) : (
                      <tr>
                        <td colSpan={5} className="px-6 py-12 text-center text-matte-text-muted">
                          No stock transactions found
                        </td>
                      </tr>
                    )}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>
    </DashboardLayout>
  );
}
