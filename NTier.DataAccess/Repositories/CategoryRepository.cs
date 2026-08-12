using Microsoft.EntityFrameworkCore;
using NTier.DataAccess.Context;
using NTier.Entities.Models;

namespace NTier.DataAccess.Repositories;

public class CategoryRepository(ADBContext context) : GenericRepository<Category>(context)
{
    public override Category? GetByID(Guid ID)
    {
        return _context.Categories
            .Include(c => c.Products)
            .FirstOrDefault(c => c.ID == ID);
    }

    public override IEnumerable<Category> GetAll()
    {
        return _context.Categories
            .Include(c => c.Products)
            .ToList();
    }
}