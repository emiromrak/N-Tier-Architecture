using NTier.DataAccess.Abstractions;
using NTier.Entities.Abstractions;
using System.Linq.Expressions;
using NTier.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace NTier.DataAccess.Repositories
{
    public class GenericRepository<T>(ADBContext context) : IRepository<T> where T : Entity
    {
        private readonly ADBContext context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();
        public void Create(T entity)
        {
            _dbSet.Add(entity);
            context.SaveChanges();
        }

        public void DeleteByID(Guid ID)
        {
            _dbSet.Remove(_dbSet.Find(ID)??throw new Exception("Entity not found"));
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return [.. _dbSet];
        }

        public T? GetByID(Guid ID)
        {
            return _dbSet.Find(ID) ?? throw new Exception("Entity not found");
        }

        public bool IfEntityExists(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public void Update(T entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            _dbSet.Update(entity);
            context?.SaveChanges();
        }
    }
}