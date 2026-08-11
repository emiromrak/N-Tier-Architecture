import { useEffect, useState } from 'react';
import { categoryService, type Category } from '../services/category.service';

export default function CategoryList() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    categoryService.getAll().then((result) => {
      if (result.error !== null) {
        setError(result.error);
      } else {
        setCategories(result.data ?? []);
      }
      setLoading(false);
    });
  }, []);

  if (loading) {
    return (
      <div className="d-flex justify-content-center align-items-center py-5">
        <div className="spinner-border text-primary me-2" role="status">
          <span className="visually-hidden">Yükleniyor...</span>
        </div>
        <span className="text-secondary fs-5">Kategoriler Yükleniyor...</span>
      </div>
    );
  }

  if (error) {
    return (
      <div className="alert alert-danger my-3" role="alert">
        <strong>Hata:</strong> {error}
      </div>
    );
  }

  return (
    <div className="card text-bg-dark border-secondary shadow-sm">
      <div className="card-header border-secondary d-flex justify-content-between align-items-center py-3">
        <h5 className="mb-0 font-bold">Kategori Listesi</h5>
        <span className="badge bg-primary rounded-pill">{categories.length} Kategori</span>
      </div>
      <div className="table-responsive">
        <table className="table table-dark table-hover mb-0 align-middle">
          <thead>
            <tr>
              <th scope="col" className="text-secondary">#</th>
              <th scope="col">Kategori Adı</th>
              <th scope="col">Açıklama</th>
              <th scope="col">Durum</th>
            </tr>
          </thead>
          <tbody>
            {categories.length === 0 ? (
              <tr>
                <td colSpan={4} className="text-center text-secondary py-4">
                  Kayıtlı kategori bulunamadı.
                </td>
              </tr>
            ) : (
              categories.map((cat, index) => (
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
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
