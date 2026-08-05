using FluentValidation.Results;
using NTier.Business.Abstractions;
using NTier.Business.Validators;
using NTier.DataAccess.Repositories;
using NTier.Entities.Models;

namespace NTier.Business.Services
{
    public class CategoryService(CategoryRepository carRepo) : IManager<Category>
    {

        private readonly CategoryRepository _repository = carRepo;
        public void Create(Category entity)
        {
            if (IfEntityExists(entity))
                throw new Exception("Bu kategori daha önce kayıt edilmiştir.");
            ValidationResult result = new CategoryValidator().Validate(entity);
            if (!result.IsValid)
                throw new Exception(string.Join("\n", result.Errors));
            _repository.Create(entity);
        }

        public void Delete(Guid id)
        {
            var cat = _repository.GetByID(id);
            if (cat != null && cat.IsActive)
                throw new Exception("Aktif olan bir kategori silinemez.");
            _repository.DeleteByID(id);
        }
            
        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll() ?? throw new Exception("Kategori bulunmamaktadır.");
        }

        public Category? GetById(Guid id)
        {
            return _repository.GetByID(id);
        }

        public bool IfEntityExists(Category entity)
        {
            return _repository.IfEntityExists(c => c.Name == entity.Name);
        }

        public void Update(Category entity)
        {
            ValidationResult result = new CategoryValidator().Validate(entity);
            if (!result.IsValid)
                throw new Exception(string.Join("\n", result.Errors));
            if (entity != null)
                _repository.Update(entity);
        }
    }
}
