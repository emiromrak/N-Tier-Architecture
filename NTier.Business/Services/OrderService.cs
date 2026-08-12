using FluentValidation.Results;
using NTier.Business.Abstractions;
using NTier.Business.Validators;
using NTier.DataAccess.Repositories;
using NTier.Entities.Models;

namespace NTier.Business.Services
{
    public class OrderService(OrderRepository orderRepo) : IManager<Order>
    {

        private readonly OrderRepository _repository = orderRepo;
        public void Create(Order entity)
        {
            if (IfEntityExists(entity))
                throw new Exception("Bu sipariş daha önce kayıt edilmiştir.");
            ValidationResult result = new OrderValidator().Validate(entity);
            if (!result.IsValid)
                throw new Exception(string.Join("\n", result.Errors));
            _repository.Create(entity);
        }

        public void Delete(Guid id)
        {
            var order = _repository.GetByID(id);
            if (order is null)
                throw new KeyNotFoundException("Sipariş bulunamadı.");
            _repository.DeleteByID(id);
        }
            
        public IEnumerable<Order> GetAll()
        {
            return _repository.GetAll();
        }

        public Order? GetById(Guid id)
        {
            return _repository.GetByID(id);
        }

        public bool IfEntityExists(Order entity)
        {
            return _repository.IfEntityExists(c => c.CustomerId == entity.CustomerId && c.OrderDate == entity.OrderDate && c.ID != entity.ID);
        }

        public void Update(Order entity)
        {
            ValidationResult result = new OrderValidator().Validate(entity);
            if (!result.IsValid)
                throw new Exception(string.Join("\n", result.Errors));
            if (_repository.GetByID(entity.ID) is null)
                throw new KeyNotFoundException("Sipariş bulunamadı.");

            if (IfEntityExists(entity))
                throw new Exception("Bu sipariş daha önce kayıt edilmiştir.");

            _repository.Update(entity);
        }
    }
}
