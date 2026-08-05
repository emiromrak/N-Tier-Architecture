using NTier.Entities.Abstractions;

namespace NTier.Entities.Models;

public class Product : Entity
{
    public string? Name { get; set; }
    public double UnitPrice { get; set; }
    public bool Discontinued { get; set; }
    public int UnitInStock { get; set; }

    //Navigation property
    public Category? Category { get; set; }
    public Guid? CategoryID { get; set; }

}