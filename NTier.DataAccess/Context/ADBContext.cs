using Microsoft.EntityFrameworkCore;
using NTier.Entities.Models;

namespace NTier.DataAccess.Context;

public class ADBContext : DbContext
{
    public ADBContext()
    {
    }

    public ADBContext(DbContextOptions<ADBContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=NTierDB;Integrated Security=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Navigation(x => x.Category).AutoInclude();
        modelBuilder.Entity<Order>().Navigation(x => x.Products).AutoInclude();
        modelBuilder.Entity<Order>().Navigation(x => x.Customer).AutoInclude();
        modelBuilder.Entity<Product>().HasQueryFilter(product => !product.IsDeleted);
        modelBuilder.Entity<Category>().HasQueryFilter(category => !category.IsDeleted);
        modelBuilder.Entity<Customer>().HasQueryFilter(customer => !customer.IsDeleted);
        modelBuilder.Entity<Order>().HasQueryFilter(order => !order.IsDeleted);

        base.OnModelCreating(modelBuilder);
    }
}
