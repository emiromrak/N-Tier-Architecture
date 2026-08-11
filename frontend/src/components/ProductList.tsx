import { useCallback, useEffect, useState } from 'react';
import {
  ProductService,
  type Product,
  type CreateProductDto,
  type UpdateProductDto,
} from '../services/product.service';
import { categoryService, type Category } from '../services/category.service';
import { Plus, Pencil, Trash2, Search, RefreshCw } from 'lucide-react';

export default function ProductList() {
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [search, setSearch] = useState('');
  const [selectedCategoryFilter, setSelectedCategoryFilter] = useState<string>('');

  // Modal states for Create / Edit
  const [showModal, setShowModal] = useState(false);
  const [editProduct, setEditProduct] = useState<Product | null>(null);
  const [name, setName] = useState('');
  const [unitPrice, setUnitPrice] = useState<number>(0);
  const [unitInStock, setUnitInStock] = useState<number>(0);
  const [discontinued, setDiscontinued] = useState(false);
  const [categoryID, setCategoryID] = useState<string>('');
  const [saving, setSaving] = useState(false);

  // Modal state for Delete confirmation
  const [deleteProduct, setDeleteProduct] = useState<Product | null>(null);
  const [deleting, setDeleting] = useState(false);

  const fetchProductsAndCategories = useCallback(async () => {
    setLoading(true);
    setError(null);
    const [pResult, cResult] = await Promise.all([
      ProductService.getAllWithCategory(),
      categoryService.getAll(),
    ]);

    if (pResult.error) {
      setError(pResult.error);
    } else {
      setProducts(pResult.data ?? []);
    }

    if (cResult.data) {
      setCategories(cResult.data);
    }
    setLoading(false);
  }, []);

  useEffect(() => {
    fetchProductsAndCategories();
  }, [fetchProductsAndCategories]);

  const openCreateModal = () => {
    setEditProduct(null);
    setName('');
    setUnitPrice(0);
    setUnitInStock(0);
    setDiscontinued(false);
    setCategoryID('');
    setShowModal(true);
  };

  const openEditModal = (product: Product) => {
    setEditProduct(product);
    setName(product.urun_ad || '');
    setUnitPrice(product.unitPrice || 0);
    setUnitInStock(product.unitInStock || 0);
    setDiscontinued(product.discontinued || false);
    setCategoryID(product.categoryID || '');
    setShowModal(true);
  };

  const handleSave = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!name.trim()) return;

    setSaving(true);
    setError(null);

    const targetCategoryID = categoryID === '' ? null : categoryID;

    if (editProduct) {
      const dto: UpdateProductDto = {
        name,
        unitPrice: Number(unitPrice),
        unitInStock: Number(unitInStock),
        discontinued,
        categoryID: targetCategoryID,
      };
      const res = await ProductService.update(editProduct.id, dto);
      if (res.error) {
        setError(`Güncelleme hatası: ${res.error}`);
      } else {
        setSuccess('Ürün başarıyla güncellendi.');
        setShowModal(false);
        fetchProductsAndCategories();
      }
    } else {
      const dto: CreateProductDto = {
        name,
        unitPrice: Number(unitPrice),
        unitInStock: Number(unitInStock),
        discontinued,
        categoryID: targetCategoryID,
      };
      const res = await ProductService.create(dto);
      if (res.error) {
        setError(`Ekleme hatası: ${res.error}`);
      } else {
        setSuccess('Yeni ürün başarıyla eklendi.');
        setShowModal(false);
        fetchProductsAndCategories();
      }
    }
    setSaving(false);
  };

  const handleDelete = async () => {
    if (!deleteProduct) return;
    setDeleting(true);
    setError(null);

    const res = await ProductService.delete(deleteProduct.id);
    if (res.error) {
      setError(`Silme hatası: ${res.error}`);
    } else {
      setSuccess('Ürün silindi.');
      setDeleteProduct(null);
      fetchProductsAndCategories();
    }
    setDeleting(false);
  };

  const filteredProducts = products.filter((p) => {
    const matchesSearch = (p.urun_ad || '').toLowerCase().includes(search.toLowerCase());
    const matchesCategory =
      selectedCategoryFilter === '' || p.categoryID === selectedCategoryFilter;
    return matchesSearch && matchesCategory;
  });

  return (
    <div className="card text-bg-dark border-secondary shadow-sm">
      {/* Header Controls */}
      <div className="card-header border-secondary d-flex flex-wrap justify-content-between align-items-center gap-3 py-3">
        <div className="d-flex align-items-center gap-3">
          <h5 className="mb-0 fw-bold">Ürün Listesi</h5>
          <span className="badge bg-primary rounded-pill">{products.length} Ürün</span>
        </div>

        <div className="d-flex flex-wrap align-items-center gap-2">
          {/* Category Filter */}
          <select
            className="form-select form-select-sm bg-dark text-white border-secondary"
            style={{ width: '170px' }}
            value={selectedCategoryFilter}
            onChange={(e) => setSelectedCategoryFilter(e.target.value)}
          >
            <option value="">Tüm Kategoriler</option>
            {categories.map((c) => (
              <option key={c.id} value={c.id}>
                {c.name}
              </option>
            ))}
          </select>

          {/* Search bar */}
          <div className="input-group input-group-sm" style={{ width: '200px' }}>
            <span className="input-group-text bg-dark border-secondary text-secondary">
              <Search size={14} />
            </span>
            <input
              type="text"
              className="form-control bg-dark text-white border-secondary"
              placeholder="Ürün Ara..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
            />
          </div>

          <button
            type="button"
            className="btn btn-sm btn-outline-secondary d-flex align-items-center gap-1"
            onClick={fetchProductsAndCategories}
            title="Yenile"
          >
            <RefreshCw size={14} />
          </button>

          <button
            type="button"
            className="btn btn-sm btn-primary d-flex align-items-center gap-1"
            onClick={openCreateModal}
          >
            <Plus size={16} />
            <span>Yeni Ürün</span>
          </button>
        </div>
      </div>

      {/* Alerts */}
      {success && (
        <div className="alert alert-success alert-dismissible fade show m-3 mb-0" role="alert">
          {success}
          <button
            type="button"
            className="btn-close"
            onClick={() => setSuccess(null)}
            aria-label="Kapat"
          ></button>
        </div>
      )}

      {error && (
        <div className="alert alert-danger alert-dismissible fade show m-3 mb-0" role="alert">
          <strong>Hata:</strong> {error}
          <button
            type="button"
            className="btn-close"
            onClick={() => setError(null)}
            aria-label="Kapat"
          ></button>
        </div>
      )}

      {/* Main Table */}
      <div className="table-responsive">
        <table className="table table-dark table-hover mb-0 align-middle">
          <thead>
            <tr>
              <th scope="col" className="text-secondary" style={{ width: '60px' }}>
                #
              </th>
              <th scope="col">Ürün Adı</th>
              <th scope="col">Kategori</th>
              <th scope="col">Birim Fiyat</th>
              <th scope="col">Stok Miktarı</th>
              <th scope="col">Durum</th>
              <th scope="col" className="text-end" style={{ width: '130px' }}>
                İşlemler
              </th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr>
                <td colSpan={7} className="text-center py-5">
                  <div className="spinner-border text-primary me-2" role="status">
                    <span className="visually-hidden">Yükleniyor...</span>
                  </div>
                  <span className="text-secondary">Ürünler Yükleniyor...</span>
                </td>
              </tr>
            ) : filteredProducts.length === 0 ? (
              <tr>
                <td colSpan={7} className="text-center text-secondary py-4">
                  {search || selectedCategoryFilter
                    ? 'Filtrenize uygun ürün bulunamadı.'
                    : 'Kayıtlı ürün bulunamadı.'}
                </td>
              </tr>
            ) : (
              filteredProducts.map((product, index) => (
                <tr key={product.id}>
                  <td className="text-secondary font-monospace">{index + 1}</td>
                  <td className="fw-semibold text-white">{product.urun_ad}</td>
                  <td>
                    <span className="badge bg-info-subtle text-info border border-info">
                      {product.kategori_ad || 'Genel'}
                    </span>
                  </td>
                  <td className="text-emerald-400 font-bold">
                    ₺{Number(product.unitPrice).toLocaleString('tr-TR', { minimumFractionDigits: 2 })}
                  </td>
                  <td>
                    <span
                      className={`badge ${
                        product.unitInStock > 10
                          ? 'bg-success-subtle text-success'
                          : product.unitInStock > 0
                          ? 'bg-warning-subtle text-warning'
                          : 'bg-danger-subtle text-danger'
                      }`}
                    >
                      {product.unitInStock} adet
                    </span>
                  </td>
                  <td>
                    {product.isActive ? (
                      <span className="badge bg-success-subtle text-success border border-success">
                        Aktif
                      </span>
                    ) : (
                      <span className="badge bg-secondary-subtle text-secondary border border-secondary">
                        Pasif
                      </span>
                    )}
                  </td>
                  <td className="text-end">
                    <div className="btn-group btn-group-sm">
                      <button
                        type="button"
                        className="btn btn-outline-warning"
                        onClick={() => openEditModal(product)}
                        title="Düzenle"
                      >
                        <Pencil size={14} />
                      </button>
                      <button
                        type="button"
                        className="btn btn-outline-danger"
                        onClick={() => setDeleteProduct(product)}
                        title="Sil"
                      >
                        <Trash2 size={14} />
                      </button>
                    </div>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      {/* CREATE / EDIT MODAL */}
      {showModal && (
        <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.6)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content bg-dark text-white border-secondary">
              <div className="modal-header border-secondary">
                <h5 className="modal-title">
                  {editProduct ? 'Ürün Düzenle' : 'Yeni Ürün Ekle'}
                </h5>
                <button
                  type="button"
                  className="btn-close btn-close-white"
                  onClick={() => setShowModal(false)}
                ></button>
              </div>
              <form onSubmit={handleSave}>
                <div className="modal-body">
                  <div className="mb-3">
                    <label className="form-label text-secondary">Ürün Adı *</label>
                    <input
                      type="text"
                      className="form-control bg-dark text-white border-secondary"
                      value={name}
                      onChange={(e) => setName(e.target.value)}
                      required
                    />
                  </div>

                  <div className="row">
                    <div className="col-md-6 mb-3">
                      <label className="form-label text-secondary">Birim Fiyat (₺)</label>
                      <input
                        type="number"
                        step="0.01"
                        min="0"
                        className="form-control bg-dark text-white border-secondary"
                        value={unitPrice}
                        onChange={(e) => setUnitPrice(parseFloat(e.target.value) || 0)}
                      />
                    </div>
                    <div className="col-md-6 mb-3">
                      <label className="form-label text-secondary">Stok Miktarı</label>
                      <input
                        type="number"
                        min="0"
                        className="form-control bg-dark text-white border-secondary"
                        value={unitInStock}
                        onChange={(e) => setUnitInStock(parseInt(e.target.value, 10) || 0)}
                      />
                    </div>
                  </div>

                  <div className="mb-3">
                    <label className="form-label text-secondary">Kategori</label>
                    <select
                      className="form-select bg-dark text-white border-secondary"
                      value={categoryID}
                      onChange={(e) => setCategoryID(e.target.value)}
                    >
                      <option value="">Kategori Yok (Genel)</option>
                      {categories.map((c) => (
                        <option key={c.id} value={c.id}>
                          {c.name}
                        </option>
                      ))}
                    </select>
                  </div>

                  <div className="form-check form-switch mb-3">
                    <input
                      className="form-check-input"
                      type="checkbox"
                      role="switch"
                      id="discontinuedSwitch"
                      checked={discontinued}
                      onChange={(e) => setDiscontinued(e.target.checked)}
                    />
                    <label className="form-check-label text-white" htmlFor="discontinuedSwitch">
                      Satıştan Kaldırıldı (Discontinued)
                    </label>
                  </div>
                </div>
                <div className="modal-footer border-secondary">
                  <button
                    type="button"
                    className="btn btn-secondary"
                    onClick={() => setShowModal(false)}
                  >
                    İptal
                  </button>
                  <button type="submit" className="btn btn-primary" disabled={saving}>
                    {saving ? 'Kaydediliyor...' : editProduct ? 'Güncelle' : 'Kaydet'}
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}

      {/* DELETE CONFIRMATION MODAL */}
      {deleteProduct && (
        <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.6)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content bg-dark text-white border-secondary">
              <div className="modal-header border-secondary">
                <h5 className="modal-title text-danger">Ürün Silinsin mi?</h5>
                <button
                  type="button"
                  className="btn-close btn-close-white"
                  onClick={() => setDeleteProduct(null)}
                ></button>
              </div>
              <div className="modal-body">
                <p className="mb-0">
                  <strong>{deleteProduct.urun_ad}</strong> isimli ürünü silmek istediğinizden emin
                  misiniz? Bu işlem geri alınamaz.
                </p>
              </div>
              <div className="modal-footer border-secondary">
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={() => setDeleteProduct(null)}
                >
                  Vazgeç
                </button>
                <button
                  type="button"
                  className="btn btn-danger"
                  onClick={handleDelete}
                  disabled={deleting}
                >
                  {deleting ? 'Siliniyor...' : 'Evet, Sil'}
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
