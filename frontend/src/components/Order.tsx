import { useCallback, useEffect, useState } from 'react';
import { orderService, type Order, type OrderProduct } from '../services/order.service';
import { Search, RefreshCw, Eye, Trash2, ShoppingCart, User, Calendar, Receipt } from 'lucide-react';

export default function OrderList() {
  const [orders, setOrders] = useState<Order[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [search, setSearch] = useState('');

  // Modal state for viewing order details
  const [viewOrder, setViewOrder] = useState<Order | null>(null);

  // Modal state for Delete confirmation
  const [deleteOrder, setDeleteOrder] = useState<Order | null>(null);
  const [deleting, setDeleting] = useState(false);

  const fetchOrders = useCallback(async () => {
    setLoading(true);
    setError(null);
    const result = await orderService.getAll();
    if (result.error) {
      setError(result.error);
    } else {
      setOrders(result.data ?? []);
    }
    setLoading(false);
  }, []);

  useEffect(() => {
    let ignore = false;
    async function startFetching() {
      const result = await orderService.getAll();
      if (!ignore) {
        if (result.error) {
          setError(result.error);
        } else {
          setOrders(result.data ?? []);
        }
        setLoading(false);
      }
    }
    startFetching();
    return () => {
      ignore = true;
    };
  }, []);

  const handleDelete = async () => {
    if (!deleteOrder) return;
    setDeleting(true);
    setError(null);

    const res = await orderService.delete(deleteOrder.id);
    if (res.error) {
      setError(`Silme hatası: ${res.error}`);
    } else {
      setSuccess('Sipariş silindi.');
      setDeleteOrder(null);
      fetchOrders();
    }
    setDeleting(false);
  };

  const filteredOrders = orders.filter((o) => {
    const customerMatch = (o.customerName || '').toLowerCase().includes(search.toLowerCase());
    const dateMatch = (o.orderDate || '').toLowerCase().includes(search.toLowerCase());
    const productMatch = (o.products || [])
      .some((p) => p.name.toLowerCase().includes(search.toLowerCase()));
    return customerMatch || dateMatch || productMatch;
  });

  const formatDate = (dateStr: string) => {
    if (!dateStr) return '-';
    const date = new Date(dateStr);
    return date.toLocaleDateString('tr-TR', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    });
  };

  const formatCurrency = (value: number) =>
    `₺${Number(value).toLocaleString('tr-TR', { minimumFractionDigits: 2 })}`;

  return (
    <div className="card text-bg-dark border-secondary shadow-sm">
      {/* Header Controls */}
      <div className="card-header border-secondary d-flex flex-wrap justify-content-between align-items-center gap-3 py-3">
        <div className="d-flex align-items-center gap-3">
          <h5 className="mb-0 fw-bold">Sipariş Listesi</h5>
          <span className="badge bg-primary rounded-pill">{orders.length} Sipariş</span>
        </div>

        <div className="d-flex flex-wrap align-items-center gap-2">
          {/* Search bar */}
          <div className="input-group input-group-sm" style={{ width: '240px' }}>
            <span className="input-group-text bg-dark border-secondary text-secondary">
              <Search size={14} />
            </span>
            <input
              type="text"
              className="form-control bg-dark text-white border-secondary placeholder-gray-500"
              placeholder="Müşteri, Tarih veya Ürün Ara..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
            />
          </div>

          <button
            type="button"
            className="btn btn-sm btn-outline-secondary d-flex align-items-center gap-1"
            onClick={fetchOrders}
            title="Yenile"
          >
            <RefreshCw size={14} />
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
              <th scope="col">Müşteri</th>
              <th scope="col">Sipariş Tarihi</th>
              <th scope="col">Ürün Sayısı</th>
              <th scope="col">Toplam Tutar</th>
              <th scope="col" className="text-end" style={{ width: '130px' }}>
                İşlemler
              </th>
            </tr>
          </thead>
          <tbody>
            {loading ? (
              <tr>
                <td colSpan={6} className="text-center py-5">
                  <div className="spinner-border text-primary me-2" role="status">
                    <span className="visually-hidden">Yükleniyor...</span>
                  </div>
                  <span className="text-secondary">Siparişler Yükleniyor...</span>
                </td>
              </tr>
            ) : filteredOrders.length === 0 ? (
              <tr>
                <td colSpan={6} className="text-center text-secondary py-4">
                  {search
                    ? 'Aramanıza uygun sipariş bulunamadı.'
                    : 'Kayıtlı sipariş bulunamadı.'}
                </td>
              </tr>
            ) : (
              filteredOrders.map((order, index) => (
                <tr
                  key={order.id}
                  className="cursor-pointer"
                  onClick={() => setViewOrder(order)}
                  role="button"
                  title="Detayları Görüntüle"
                >
                  <td className="text-secondary font-monospace">{index + 1}</td>
                  <td className="fw-semibold text-white">
                    <span className="d-flex align-items-center gap-2">
                      <User size={14} className="text-info shrink-0" />
                      {order.customerName || 'Bilinmiyor'}
                    </span>
                  </td>
                  <td className="text-secondary">
                    <span className="d-flex align-items-center gap-2">
                      <Calendar size={14} className="text-warning shrink-0" />
                      {formatDate(order.orderDate)}
                    </span>
                  </td>
                  <td>
                    <span className="badge bg-secondary-subtle text-secondary border border-secondary">
                      {order.products?.length ?? 0} Ürün
                    </span>
                  </td>
                  <td className="text-emerald-400 font-bold">
                    {formatCurrency(order.totalAmount)}
                  </td>
                  <td className="text-end" onClick={(e) => e.stopPropagation()}>
                    <div className="btn-group btn-group-sm">
                      <button
                        type="button"
                        className="btn btn-outline-info"
                        onClick={() => setViewOrder(order)}
                        title="Detayları Görüntüle"
                      >
                        <Eye size={14} />
                      </button>
                      <button
                        type="button"
                        className="btn btn-outline-danger"
                        onClick={() => setDeleteOrder(order)}
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

      {/* VIEW ORDER DETAILS MODAL */}
      {viewOrder && (
        <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.6)' }}>
          <div className="modal-dialog modal-dialog-centered modal-lg">
            <div className="modal-content bg-dark text-white border-secondary">
              <div className="modal-header border-secondary">
                <h5 className="modal-title d-flex align-items-center gap-2">
                  <Receipt size={20} className="text-info" />
                  <span>Sipariş Detayı</span>
                </h5>
                <button
                  type="button"
                  className="btn-close btn-close-white"
                  onClick={() => setViewOrder(null)}
                ></button>
              </div>
              <div className="modal-body">
                {/* Order Summary Info */}
                <div className="row g-3 mb-4">
                  <div className="col-md-4">
                    <div className="p-3 rounded-3 border border-secondary bg-black-50">
                      <div className="d-flex align-items-center gap-2 text-secondary small mb-1">
                        <User size={14} />
                        <span>Müşteri</span>
                      </div>
                      <div className="fw-bold text-white">
                        {viewOrder.customerName || 'Bilinmiyor'}
                      </div>
                    </div>
                  </div>
                  <div className="col-md-4">
                    <div className="p-3 rounded-3 border border-secondary bg-black-50">
                      <div className="d-flex align-items-center gap-2 text-secondary small mb-1">
                        <Calendar size={14} />
                        <span>Sipariş Tarihi</span>
                      </div>
                      <div className="fw-bold text-white">
                        {formatDate(viewOrder.orderDate)}
                      </div>
                    </div>
                  </div>
                  <div className="col-md-4">
                    <div className="p-3 rounded-3 border border-success bg-black-50">
                      <div className="d-flex align-items-center gap-2 text-secondary small mb-1">
                        <ShoppingCart size={14} />
                        <span>Toplam Tutar</span>
                      </div>
                      <div className="fw-bold text-emerald-400">
                        {formatCurrency(viewOrder.totalAmount)}
                      </div>
                    </div>
                  </div>
                </div>

                {/* Products in this order */}
                <h6 className="text-secondary text-uppercase small fw-bold mb-2">
                  Sipariş Edilen Ürünler
                </h6>
                <div className="table-responsive">
                  <table className="table table-dark table-hover mb-0 align-middle">
                    <thead>
                      <tr>
                        <th scope="col" className="text-secondary" style={{ width: '60px' }}>
                          #
                        </th>
                        <th scope="col">Ürün Adı</th>
                        <th scope="col">Birim Fiyat</th>
                        <th scope="col">Stok Durumu</th>
                      </tr>
                    </thead>
                    <tbody>
                      {viewOrder.products && viewOrder.products.length > 0 ? (
                        viewOrder.products.map((product: OrderProduct, idx: number) => (
                          <tr key={product.id}>
                            <td className="text-secondary font-monospace">{idx + 1}</td>
                            <td className="fw-semibold text-white">{product.name}</td>
                            <td className="text-emerald-400 font-bold">
                              {formatCurrency(product.unitPrice)}
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
                          </tr>
                        ))
                      ) : (
                        <tr>
                          <td colSpan={4} className="text-center text-secondary py-4">
                            Bu siparişte ürün bulunmamaktadır.
                          </td>
                        </tr>
                      )}
                    </tbody>
                  </table>
                </div>
              </div>
              <div className="modal-footer border-secondary">
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={() => setViewOrder(null)}
                >
                  Kapat
                </button>
              </div>
            </div>
          </div>
        </div>
      )}

      {/* DELETE CONFIRMATION MODAL */}
      {deleteOrder && (
        <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.6)' }}>
          <div className="modal-dialog modal-dialog-centered">
            <div className="modal-content bg-dark text-white border-secondary">
              <div className="modal-header border-secondary">
                <h5 className="modal-title text-danger">Sipariş Silinsin mi?</h5>
                <button
                  type="button"
                  className="btn-close btn-close-white"
                  onClick={() => setDeleteOrder(null)}
                ></button>
              </div>
              <div className="modal-body">
                <p className="mb-0">
                  <strong>{deleteOrder.customerName || 'Bu sipariş'}</strong> müşterisine ait{' '}
                  <strong>{formatDate(deleteOrder.orderDate)}</strong> tarihli{' '}
                  <strong>
                    {formatCurrency(deleteOrder.totalAmount)}
                  </strong>{' '}
                  tutarındaki siparişi silmek istediğinizden emin misiniz? Bu işlem geri alınamaz.
                </p>
              </div>
              <div className="modal-footer border-secondary">
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={() => setDeleteOrder(null)}
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