using Microsoft.EntityFrameworkCore;
using NTier.DataAccess.Context;
using NTier.Entities.Models;

namespace NTier.DataAccess.Repositories;

public class OrderRepository(ADBContext context) : GenericRepository<Order>(context)
{
    public override IEnumerable<Order> GetAll()
    {
        return _context.Orders
            .IgnoreQueryFilters()
            .Where(o => !o.IsDeleted)
            .Include(o => o.Customer)
            .Include(o => o.Products)
            .ToList();
    }

    public override Order? GetByID(Guid ID)
    {
        return _context.Orders
            .IgnoreQueryFilters()
            .Where(o => !o.IsDeleted)
            .Include(o => o.Customer)
            .Include(o => o.Products)
            .FirstOrDefault(o => o.ID == ID);
    }

    public override void DeleteByID(Guid ID)
    {
        var entity = GetByID(ID) ?? throw new KeyNotFoundException("Order not found.");
        entity.IsDeleted = true;
        entity.UpdatedDate = DateTime.UtcNow;
        _context.SaveChanges();
    }

    public override void Update(Order entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _context.Orders.Update(entity);
        }
        _context.SaveChanges();
    }
}