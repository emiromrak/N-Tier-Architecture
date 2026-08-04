using NTier.Entities.Abstractions;

namespace NTier.Business.Abstractions;

public interface IManager<T> where T : Entity
{
    void Create(T entity);
    void Update(T entity);
    void Delete(Guid id);
    T? GetById(Guid id);
    IEnumerable<T> GetAll();
    bool IfEntityExists(T entity);
}