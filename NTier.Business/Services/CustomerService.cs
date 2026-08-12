using FluentValidation.Results;
using NTier.Business.Abstractions;
using NTier.Business.Validators;
using NTier.DataAccess.Repositories;
using NTier.Entities.Models;

namespace NTier.Business.Services
{
    public class CustomerService(CustomerRepository CustomerRepo) : IManager<Customer>
    {

        private readonly CustomerRepository _repository = CustomerRepo;
        public void Create(Customer entity)
        {
            if (IfEntityExists(entity))
                throw new Exception("Bu kategori daha önce kayıt edilmiştir.");
            ValidationResult result = new CustomerValidator().Validate(entity);
            if (!result.IsValid)
                throw new Exception(string.Join("\n", result.Errors));
            _repository.Create(entity);
        }

        public void Delete(Guid id)
        {
            var Customer = _repository.GetByID(id);
            if (Customer is null)
                throw new KeyNotFoundException("Müşteri bulunamadı.");
            _repository.DeleteByID(id);
        }
            
        public IEnumerable<Customer> GetAll()
        {
            return _repository.GetAll();
        }

        public Customer? GetById(Guid id)
        {
            return _repository.GetByID(id);
        }

        public bool IfEntityExists(Customer entity)
        {
            return _repository.IfEntityExists(c => c.Name == entity.Name && c.ID != entity.ID);
        }

        public void Update(Customer entity)
        {
            ValidationResult result = new CustomerValidator().Validate(entity);
            if (!result.IsValid)
                throw new Exception(string.Join("\n", result.Errors));
            if (_repository.GetByID(entity.ID) is null)
                throw new KeyNotFoundException("Müşteri bulunamadı.");

            if (IfEntityExists(entity))
                throw new Exception("Bu müşteri daha önce kayıt edilmiştir.");

            _repository.Update(entity);
        }
    }
}
