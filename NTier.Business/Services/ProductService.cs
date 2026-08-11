using FluentValidation.Results;
using NTier.Business.Abstractions;
using NTier.Business.Validators;
using NTier.DataAccess.Repositories;
using NTier.Entities.Models;

namespace NTier.Business.Services
{
    public class ProductService(ProductRepository proRepo) : IManager<Product>
    {
        private readonly ProductRepository _repository = proRepo;
        public void Create(Product entity)
        {
            if (IfEntityExists(entity))
                throw new Exception("Bu ürün daha önce kayıt edilmiştir.");
            ValidationResult result = new ProductValidator().Validate(entity);
            if (!result.IsValid)
                throw new Exception(string.Join("\n", result.Errors));
            _repository.Create(entity);
        }

        public void Delete(Guid id)
        {
            var product = _repository.GetByID(id);
            if (product is null)
                throw new KeyNotFoundException("Ürün bulunamadı.");
            if (product.IsActive)
                throw new Exception("Aktif olan bir ürün silinemez.");
            _repository.DeleteByID(id);
        }


        public IEnumerable<Product> GetAll()
        {
            return _repository.GetAll();
        }

        public Product? GetById(Guid id)
        {
            return _repository.GetByID(id);
        }

        public bool IfEntityExists(Product entity)
        {
            return _repository.IfEntityExists(product =>
                product.Name == entity.Name &&
                product.CategoryID == entity.CategoryID &&
                product.ID != entity.ID);
        }   

        public void Update(Product entity)
        {
            ValidationResult result = new ProductValidator().Validate(entity);
            if (!result.IsValid)
                throw new Exception(string.Join("\n", result.Errors));
            if (_repository.GetByID(entity.ID) is null)
                throw new KeyNotFoundException("Ürün bulunamadı.");

            if (IfEntityExists(entity))
                throw new Exception("Bu ürün daha önce kayıt edilmiştir.");

            _repository.Update(entity);
        }
    }
}
