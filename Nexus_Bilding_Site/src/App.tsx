import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import {Login, Register, ForgotPassword} from './modules/auth/pages';
import { Clients, ClientDetails } from './modules/clients/pages/index';
import { Products, ProductDetails } from './modules/inventory/pages/index';
import { Dashboard } from './modules/dashboard/pages/Dashboard';
import { Invoices } from './modules/sales/pages/Invoices';
import { InvoiceDetails } from './modules/sales/pages/InvoiceDetails';
import { Reports } from './modules/reports/pages/Reports';
import { Settings } from './modules/admin/pages/Settings';
import { Profile } from './modules/users/pages/Profile';
import { RoleRoute } from './modules/auth/components/RoleRoute';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/login" replace />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/forgot-password" element={<ForgotPassword />} />

        {/* Separated Role-Based Namespaces */}
        
        {/* SELLER ROUTES */}
        <Route path="/seller" element={<RoleRoute allowedRoles={['seller']} />}>
          <Route path="" element={<Navigate to="dashboard" replace />} />
          <Route path="dashboard" element={<Dashboard />} />
          <Route path="invoices" element={<Invoices />} />
          <Route path="clients" element={<Clients />} />
          <Route path="clients/:id" element={<ClientDetails />} />
          <Route path="inventory" element={<Products />} />
          <Route path="inventory/:id" element={<ProductDetails />} />
          <Route path="profile" element={<Profile />} />
        </Route>

        {/* OWNER ROUTES */}
        <Route path="/owner" element={<RoleRoute allowedRoles={['owner']} />}>
          <Route path="" element={<Navigate to="dashboard" replace />} />
          <Route path="dashboard" element={<Dashboard />} />
          <Route path="invoices" element={<Invoices />} />
          <Route path="invoices/:id" element={<InvoiceDetails />} />
          <Route path="clients" element={<Clients />} />
          <Route path="clients/:id" element={<ClientDetails />} />
          <Route path="inventory" element={<Products />} />
          <Route path="inventory/:id" element={<ProductDetails />} />
          <Route path="reports" element={<Reports />} />
          <Route path="settings" element={<Settings />} />
          <Route path="profile" element={<Profile />} />
        </Route>

        {/* ADMIN ROUTES */}
        <Route path="/admin" element={<RoleRoute allowedRoles={['admin']} />}>
          <Route path="" element={<Navigate to="dashboard" replace />} />
          <Route path="dashboard" element={<Dashboard />} />
          <Route path="invoices" element={<Invoices />} />
          <Route path="invoices/:id" element={<InvoiceDetails />} />
          <Route path="clients" element={<Clients />} />
          <Route path="clients/:id" element={<ClientDetails />} />
          <Route path="inventory" element={<Products />} />
          <Route path="inventory/:id" element={<ProductDetails />} />
          <Route path="reports" element={<Reports />} />
          <Route path="settings" element={<Settings />} />
          <Route path="profile" element={<Profile />} />
        </Route>

        <Route path="*" element={<Navigate to="/login" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
