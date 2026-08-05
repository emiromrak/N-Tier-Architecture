using Microsoft.EntityFrameworkCore;
using NTier.Entities.Models;

namespace NTier.DataAccess.Context;

public class ADBContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source = .;Initial Catalog=NTierDB;Integrated Security = true;TrustServerCertificate = true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().Navigation(x => x.Category).AutoInclude();
        modelBuilder.Entity<Category>().Navigation(x => x.Products).AutoInclude();
        base.OnModelCreating(modelBuilder);
    }
}
