using NTier.DataAccess.Context;
using NTier.Entities.Models;

namespace NTier.DataAccess.Repositories;
public class ProductRepository(ADBContext context) : GenericRepository<Product>(context);

