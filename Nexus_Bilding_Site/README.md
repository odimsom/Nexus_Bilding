# Nexus Bilding - Site

## Overview

Nexus Bilding Site is a modern web application built for comprehensive business management, specifically tailored for the Dominican Republic market with DGII compliance. It handles Invoicing (e-CF), Inventory, Client Management, and Detailed Reporting.

## Tech Stack

- **Framework**: React 18
- **Build Tool**: Vite
- **Language**: TypeScript
- **Styling**: Tailwind CSS
- **Icons**: Lucide React
- **PDF Generation**: jsPDF & jsPDF-AutoTable
- **State/Data**: Supabase (Integration ready)

## Key Features

### 📊 Dashboard
- Real-time business metrics.
- Revenue data visualization.
- Key Performance Indicators (KPIs).

### 🧾 Fiscal Invoicing (DGII Ready)
- Support for all major Fiscal Document Types:
  - **E31**: Factura de Crédito Fiscal
  - **E32**: Factura de Consumo
  - **E33**: Regímenes Especiales
  - **E34**: Gubernamental
  - **E41**: Comprobante de Compras
- Electronic Invoice (e-CF) management.

### 📦 Products & Inventory
- Product catalog management.
- Stock tracking and history.
- ITBIS configuration per product.

### 👥 Client Management
- Client directory with RNC/Cédula validation.
- Taxpayer type classification.

### 💼 Administration
- User roles (Owner, Seller, Admin).
- System settings.

## Getting Started

### Prerequisites
- Node.js (Latest LTS recommended)
- npm or yarn

### Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```

2. Navigate to the project directory:
   ```bash
   cd Nexus_Bilding_Site
   ```

3. Install dependencies:
   ```bash
   npm install
   ```

4. Run the development server:
   ```bash
   npm run dev
   ```

## Scripts

- `dev`: Starts the Vite development server.
- `build`: Builds the project for production.
- `preview`: Previews the production build locally.
- `lint`: Runs ESLint to check for code quality issues.
