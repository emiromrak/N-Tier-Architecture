using NTier.Entities.Abstractions;
using System.Linq.Expressions;

namespace NTier.DataAccess.Abstractions
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T entity);
        void Update(T entity);
        void DeleteByID(Guid ID);
        T? GetByID(Guid ID);
        IEnumerable<T> GetAll();
        bool IfEntityExists(Expression<Func<T,bool>> filter);
    }
}
