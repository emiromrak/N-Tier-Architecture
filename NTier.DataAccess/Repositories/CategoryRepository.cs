using NTier.DataAccess.Context;
using NTier.Entities.Models;

namespace NTier.DataAccess.Repositories;
public class CategoryRepository(ADBContext context) : GenericRepository<Category>(context);