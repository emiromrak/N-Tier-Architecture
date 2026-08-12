using NTier.DataAccess.Context;
using NTier.Entities.Models;

namespace NTier.DataAccess.Repositories;
public class CustomerRepository(ADBContext context) : GenericRepository<Customer>(context);