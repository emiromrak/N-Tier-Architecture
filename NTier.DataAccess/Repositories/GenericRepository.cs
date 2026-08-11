using NTier.DataAccess.Abstractions;
using NTier.Entities.Abstractions;
using System.Linq.Expressions;
using NTier.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace NTier.DataAccess.Repositories
{
    public class GenericRepository<T>(ADBContext context) : IRepository<T> where T : Entity
    {
        private readonly ADBContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();
        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteByID(Guid ID)
        {
            var entity = GetByID(ID) ?? throw new KeyNotFoundException("Entity not found.");
            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? GetByID(Guid ID)
        {
            return _dbSet.SingleOrDefault(entity => entity.ID == ID);
        }

        public bool IfEntityExists(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public void Update(T entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
