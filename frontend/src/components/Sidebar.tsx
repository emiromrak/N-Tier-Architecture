import React from 'react';
import { Box, Layers, ShoppingBag, ShoppingCart } from 'lucide-react';

interface SidebarProps {
  activeTab: 'categories' | 'products' | 'orders';
  setActiveTab: (tab: 'categories' | 'products' | 'orders') => void;
}

export const Sidebar: React.FC<SidebarProps> = ({ activeTab, setActiveTab }) => {
  return (
    <div
      className="d-flex flex-column flex-shrink-0 p-3 text-bg-dark border-end border-secondary min-vh-100"
      style={{ width: '280px' }}
    >
      <a
        href="#"
        onClick={(e) => e.preventDefault()}
        className="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-white text-decoration-none"
      >
        <Box className="me-2 text-primary" size={28} />
        <span className="fs-4 font-bold tracking-wide">NTier ERP Portal</span>
      </a>
      <hr className="border-secondary my-3" />
      <ul className="nav nav-pills flex-column mb-auto gap-1">
        <li className="nav-item">
          <button
            type="button"
            className={`nav-link w-100 text-start d-flex align-items-center ${
              activeTab === 'categories' ? 'active' : 'text-white'
            }`}
            onClick={() => setActiveTab('categories')}
          >
            <Layers className="me-2 shrink-0" size={18} />
            <span>Kategoriler</span>
          </button>
        </li>
        <li className="nav-item">
          <button
            type="button"
            className={`nav-link w-100 text-start d-flex align-items-center ${
              activeTab === 'products' ? 'active' : 'text-white'
            }`}
            onClick={() => setActiveTab('products')}
          >
            <ShoppingBag className="me-2 shrink-0" size={18} />
            <span>Ürünler</span>
          </button>
        </li>
        <li className="nav-item">
          <button
            type="button"
            className={`nav-link w-100 text-start d-flex align-items-center ${
              activeTab === 'orders' ? 'active' : 'text-white'
            }`}
            onClick={() => setActiveTab('orders')}
          >
            <ShoppingCart className="me-2 shrink-0" size={18} />
            <span>Siparişler</span>
          </button>
        </li>
      </ul>
      <hr className="border-secondary my-3" />
      <div className="dropdown">
        <a
          href="#"
          className="d-flex align-items-center text-white text-decoration-none dropdown-toggle"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          <img
            src="https://images.unsplash.com/photo-1534528741775-53994a69daeb?auto=format&fit=crop&w=100&q=80"
            alt="User avatar"
            width="32"
            height="32"
            className="rounded-circle me-2 object-cover"
          />
          <strong>Emir Omrak</strong>
        </a>
        <ul className="dropdown-menu dropdown-menu-dark text-small shadow">
          <li>
            <a className="dropdown-item" href="#">
              Ayarlar
            </a>
          </li>
          <li>
            <a className="dropdown-item" href="#">
              Profil
            </a>
          </li>
          <li>
            <hr className="dropdown-divider" />
          </li>
          <li>
            <a className="dropdown-item text-danger" href="#">
              Çıkış Yap
            </a>
          </li>
        </ul>
      </div>
    </div>
  );
};

export default Sidebar;