import { useEffect, useState } from 'react';
import { ProductService, type Product } from '../services/product.service';

export default function ProductList() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    ProductService.getAll().then((result) => {
      if (result.error !== null) {
        setError(result.error);
      } else {
        setProducts(result.data ?? []);
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
        <span className="text-secondary fs-5">Ürünler Yükleniyor...</span>
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
        <h5 className="mb-0 font-bold">Ürün Listesi</h5>
        <span className="badge bg-primary rounded-pill">{products.length} Ürün</span>
      </div>
      <div className="table-responsive">
        <table className="table table-dark table-hover mb-0 align-middle">
          <thead>
            <tr>
              <th scope="col" className="text-secondary">#</th>
              <th scope="col">Ürün Adı</th>
              <th scope="col">Kategori</th>
              <th scope="col">Birim Fiyat</th>
              <th scope="col">Stok Miktarı</th>
              <th scope="col">Durum</th>
            </tr>
          </thead>
          <tbody>
            {products.length === 0 ? (
              <tr>
                <td colSpan={6} className="text-center text-secondary py-4">
                  Kayıtlı ürün bulunamadı.
                </td>
              </tr>
            ) : (
              products.map((product, index) => (
                <tr key={product.id}>
                  <td className="text-secondary font-monospace">{index + 1}</td>
                  <td className="fw-semibold text-white">{product.urun_ad}</td>
                  <td>
                    <span className="badge bg-info-subtle text-info border border-info">
                      {product.kategori_ad || 'Genel'}
                    </span>
                  </td>
                  <td className="text-emerald-400 fw-bold">
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
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
