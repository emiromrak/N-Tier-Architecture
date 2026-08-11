import { useState } from 'react';
import Sidebar from './components/Sidebar';
import CategoryList from './components/CategoryList';
import ProductList from './components/ProductList';

function App() {
  const [activeTab, setActiveTab] = useState<'categories' | 'products'>('categories');

  return (
    <div className="d-flex min-vh-100 bg-dark text-white">
      {/* Sidebar Component */}
      <Sidebar activeTab={activeTab} setActiveTab={setActiveTab} />

      {/* Main Content Area */}
      <main className="flex-grow-1 p-4 overflow-auto">
        <div className="container-fluid max-w-5xl">
          <header className="d-flex justify-content-between align-items-center mb-4 pb-3 border-bottom border-secondary">
            <div>
              <h1 className="h3 fw-bold mb-1">
                {activeTab === 'categories' ? 'Kategori Yönetimi' : 'Ürün Yönetimi'}
              </h1>
              <p className="text-secondary mb-0 small">
                {activeTab === 'categories'
                  ? 'Sistemdeki tüm kategorileri görüntüleyin ve inceleyin.'
                  : 'Sistemdeki tüm ürünleri ve stok durumlarını görüntüleyin.'}
              </p>
            </div>
          </header>

          {/* Conditional View */}
          {activeTab === 'categories' ? <CategoryList /> : <ProductList />}
        </div>
      </main>
    </div>
  );
}

export default App;
