import { useCallback, useEffect, useState } from 'react';
import {
  categoryService,
  type Category,
  type CreateCategoryDto,
  type UpdateCategoryDto,
} from '../services/category.service';
import { type Product } from '../services/product.service';
import { Plus, Pencil, Trash2, Search, RefreshCw, ShoppingBag } from 'lucide-react';

export default function CategoryList() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [search, setSearch] = useState('');

  // Modal states for Create / Edit
  const [showModal, setShowModal] = useState(false);
  const [editCategory, setEditCategory] = useState<Category | null>(null);
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [isActive, setIsActive] = useState(true);
  const [saving, setSaving] = useState(false);

  // Modal state for Delete confirmation
  const [deleteCategory, setDeleteCategory] = useState<Category | null>(null);
  const [deleting, setDeleting] = useState(false);

  // Modal state for Viewing Category Products
  const [viewCategory, setViewCategory] = useState<Category | null>(null);
  const [categoryProducts, setCategoryProducts] = useState<Product[]>([]);
  const [loadingProducts, setLoadingProducts] = useState(false);

  const fetchCategories = useCallback(async () => {
    setLoading(true);
    setError(null);
    const result = await categoryService.getAll();
    if (result.error) {
      setError(result.error);
    } else {
      setCategories(result.data ?? []);
    }
    setLoading(false);
  }, []);

  useEffect(() => {
    let ignore = false;
    async function startFetching() {
      const result = await categoryService.getAll();
      if (!ignore) {
        if (result.error) {
          setError(result.error);
        } else {
          setCategories(result.data ?? []);
        }
        setLoading(false);
      }
    }
    startFetching();
    return () => {
      ignore = true;
    };
  }, []);

  const openCreateModal = () => {
    setEditCategory(null);
    setName('');
    setDescription('');
    setIsActive(true);
    setShowModal(true);
  };

  const openEditModal = (category: Category) => {
    setEditCategory(category);
    setName(category.name);
    setDescription(category.description);
    setIsActive(category.isActive);
    setShowModal(true);
  };

  const handleSave = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!name.trim()) return;

    setSaving(true);
    setError(null);

    if (editCategory) {
      const dto: UpdateCategoryDto = { name, description, isActive };
      const res = await categoryService.update(editCategory.id, dto);
      if (res.error) {
        setError(`Güncelleme hatası: ${res.error}`);
      } else {
        setSuccess('Kategori başarıyla güncellendi.');
        setShowModal(false);
        fetchCategories();
      }
    } else {
      const dto: CreateCategoryDto = { name, description, isActive };
      const res = await categoryService.create(dto);
      if (res.error) {
        setError(`Ekleme hatası: ${res.error}`);
      } else {
        setSuccess('Yeni kategori eklendi.');
        setShowModal(false);
        fetchCategories();
      }
    }
    setSaving(false);
  };

  const handleDelete = async () => {
    if (!deleteCategory) return;
    setDeleting(true);
    setError(null);

    const res = await categoryService.delete(deleteCategory.id);
    if (res.error) {
      setError(`Silme hatası: ${res.error}`);
    } else {
      setSuccess('Kategori silindi.');
      setDeleteCategory(null);
      fetchCategories();
    }
    setDeleting(false);
  };

  const openCategoryProducts = async (category: Category) => {
    setViewCategory(category);
    setLoadingProducts(true);
    const res = await categoryService.getProducts(category.id);
    if (res.data) {
      setCategoryProducts(res.data);
    } else {
      setCategoryProducts([]);
    }
    setLoadingProducts(false);
  };

  const filteredCategories = categories.filter(
    (c) =>
      c.name.toLowerCase().includes(search.toLowerCase()) ||
      c.description.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div className="card text-bg-dark border-secondary shadow-sm">
      {/* Header Controls */}
      <div className="card-header border-secondary d-flex flex-wrap justify-content-between align-items-center gap-3 py-3">
        <div className="d-flex align-items-center gap-3">
          <h5 className="mb-0 fw-bold">Kategori Listesi</h5>
          <span className="badge bg-primary rounded-pill">{categories.length} Kategori</span>
        </div>

        <div className="d-flex flex-wrap align-items-center gap-2">
          {/* Search bar */}
          <div className="input-group input-group-sm" style={{ width: '220px' }}>
            <span className="input-group-text bg-dark border-secondary text-secondary">
              <Search size={14} />
            </span>
            <input
              type="text"
              className="form-control bg-dark text-white border-secondary placeholder-gray-500"
              placeholder="Kategori Ara..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
            />
          </div>

          <button
            type="button"
            className="btn btn-sm btn-outline-secondary d-flex align-items-center gap-1"
            onClick={fetchCategories}
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
            <span>Yeni Kategori</span>
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
              <th scope="col">Kategori Adı</th>
              <th scope="col">Açıklama</th>
              <th scope="col">Durum</th>
              <th scope="col" className="text-end" style={{ width: '160px' }}>
                İşlemler
              </th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr>
                <td colSpan={5} className="text-center py-5">
                  <div className="spinner-border text-primary me-2" role="status">
                    <span className="visually-hidden">Yükleniyor...</span>
                  </div>
                  <span className="text-secondary">Kategoriler Yükleniyor...</span>
                </td>
              </tr>
            ) : filteredCategories.length === 0 ? (
              <tr>
                <td colSpan={5} className="text-center text-secondary py-4">
                  {search ? 'Aramanıza uygun kategori bulunamadı.' : 'Kayıtlı kategori bulunamadı.'}
                </td>
              </tr>
            ) : (
              filteredCategories.map((cat, index) => (
                <tr key={cat.id}>
                  <td className="text-secondary font-monospace">{index + 1}</td>
                  <td className="fw-semibold text-white">{cat.name}</td>
                  <td className="text-secondary">{cat.description || '-'}</td>
                  <td>
                    {cat.isActive ? (
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
                        className="btn btn-outline-info"
                        onClick={() => openCategoryProducts(cat)}
                        title="Ürünleri Göster"
                      >
                        <ShoppingBag size={14} />
                      </button>
                      <button
                        type="button"
                        className="btn btn-outline-warning"
                        onClick={() => openEditModal(cat)}
                        title="Düzenle"
                      >
                        <Pencil size={14} />
                      </button>
                      <button
                        type="button"
                        className="btn btn-outline-danger"
                        onClick={() => setDeleteCategory(cat)}
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
        <div className="modal show d-block tab-index-1" style={{ backgroundColor: 'rgba(0,0,0,0.6)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content bg-dark text-white border-secondary">
              <div className="modal-header border-secondary">
                <h5 className="modal-title">
                  {editCategory ? 'Kategori Düzenle' : 'Yeni Kategori Ekle'}
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
                    <label className="form-label text-secondary">Kategori Adı *</label>
                    <input
                      type="text"
                      className="form-control bg-dark text-white border-secondary"
                      value={name}
                      onChange={(e) => setName(e.target.value)}
                      required
                    />
                  </div>
                  <div className="mb-3">
                    <label className="form-label text-secondary">Açıklama</label>
                    <textarea
                      className="form-control bg-dark text-white border-secondary"
                      rows={3}
                      value={description}
                      onChange={(e) => setDescription(e.target.value)}
                    ></textarea>
                  </div>
                  <div className="form-check form-switch mb-3">
                    <input
                      className="form-check-input"
                      type="checkbox"
                      role="switch"
                      id="categoryActiveSwitch"
                      checked={isActive}
                      onChange={(e) => setIsActive(e.target.checked)}
                    />
                    <label className="form-check-label text-white" htmlFor="categoryActiveSwitch">
                      Aktif Durumda
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
                    {saving ? 'Kaydediliyor...' : editCategory ? 'Güncelle' : 'Kaydet'}
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}

      {/* DELETE CONFIRMATION MODAL */}
      {deleteCategory && (
        <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.6)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content bg-dark text-white border-secondary">
              <div className="modal-header border-secondary">
                <h5 className="modal-title text-danger">Kategori Silinsin mi?</h5>
                <button
                  type="button"
                  className="btn-close btn-close-white"
                  onClick={() => setDeleteCategory(null)}
                ></button>
              </div>
              <div className="modal-body">
                <p className="mb-0">
                  <strong>{deleteCategory.name}</strong> isimli kategoriyi silmek istediğinizden emin
                  misiniz? Bu işlem geri alınamaz.
                </p>
              </div>
              <div className="modal-footer border-secondary">
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={() => setDeleteCategory(null)}
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

      {/* VIEW CATEGORY PRODUCTS MODAL */}
      {viewCategory && (
        <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.6)' }}>
          <div className="modal-dialog modal-dialog-centered modal-lg">
            <div className="modal-content bg-dark text-white border-secondary">
              <div className="modal-header border-secondary">
                <h5 className="modal-title d-flex align-items-center gap-2">
                  <ShoppingBag size={20} className="text-info" />
                  <span>{viewCategory.name} - Ürünleri</span>
                </h5>
                <button
                  type="button"
                  className="btn-close btn-close-white"
                  onClick={() => setViewCategory(null)}
                ></button>
              </div>
              <div className="modal-body p-0">
                {loadingProducts ? (
                  <div className="text-center py-4">
                    <div className="spinner-border text-info me-2" role="status"></div>
                    <span className="text-secondary">Ürünler Yükleniyor...</span>
                  </div>
                ) : categoryProducts.length === 0 ? (
                  <div className="text-center text-secondary py-4">
                    Bu kategoriye ait herhangi bir ürün bulunmamaktadır.
                  </div>
                ) : (
                  <div className="table-responsive">
                    <table className="table table-dark table-hover mb-0 align-middle">
                      <thead>
                        <tr>
                          <th>#</th>
                          <th>Ürün Adı</th>
                          <th>Birim Fiyat</th>
                          <th>Stok</th>
                          <th>Durum</th>
                        </tr>
                      </thead>
                      <tbody>
                        {categoryProducts.map((p, idx) => (
                          <tr key={p.id}>
                            <td className="text-secondary font-monospace">{idx + 1}</td>
                            <td className="fw-semibold text-white">{p.urun_ad}</td>
                            <td className="text-emerald-400 font-bold">
                              ₺{Number(p.unitPrice).toLocaleString('tr-TR', { minimumFractionDigits: 2 })}
                            </td>
                            <td>{p.unitInStock} adet</td>
                            <td>
                              {p.isActive ? (
                                <span className="badge bg-success-subtle text-success">Aktif</span>
                              ) : (
                                <span className="badge bg-secondary-subtle text-secondary">Pasif</span>
                              )}
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                )}
              </div>
              <div className="modal-footer border-secondary">
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={() => setViewCategory(null)}
                >
                  Kapat
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
