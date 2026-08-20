import { useState } from 'react';
import Sidebar from './components/Sidebar';
import CategoryList from './components/CategoryList';
import ProductList from './components/ProductList';
import OrderList from './components/Order';

type Tab = 'categories' | 'products' | 'orders';

function App() {
  const [activeTab, setActiveTab] = useState<Tab>('categories');

  const titles: Record<Tab, { title: string; description: string }> = {
    categories: {
      title: 'Kategori Yönetimi',
      description: 'Sistemdeki tüm kategorileri görüntüleyin ve inceleyin.',
    },
    products: {
      title: 'Ürün Yönetimi',
      description: 'Sistemdeki tüm ürünleri ve stok durumlarını görüntüleyin.',
    },
    orders: {
      title: 'Sipariş Yönetimi',
      description: 'Sistemdeki tüm siparişleri ve sipariş edilen ürünleri görüntüleyin.',
    },
  };
  
  return (
    <div className="d-flex min-vh-100 bg-dark text-white">
      {/* Sidebar Component */}
      <Sidebar activeTab={activeTab} setActiveTab={setActiveTab} />

      {/* Main Content Area */}
      <main className="flex-grow-1 p-4 overflow-auto">
        <div className="container-fluid max-w-5xl">
          <header className="d-flex justify-content-between align-items-center mb-4 pb-3 border-bottom border-secondary">
            <div>
              <h1 className="h3 fw-bold mb-1">{titles[activeTab].title}</h1>
              <p className="text-secondary mb-0 small">{titles[activeTab].description}</p>
            </div>
          </header>

          {/* Conditional View */}
          {activeTab === 'categories' && <CategoryList />}
          {activeTab === 'products' && <ProductList />}
          {activeTab === 'orders' && <OrderList />}
        </div>
      </main>
    </div>
  );
}

export default App;