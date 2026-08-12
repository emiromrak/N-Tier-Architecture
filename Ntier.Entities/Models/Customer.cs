using NTier.Entities.Abstractions;

namespace NTier.Entities.Models;

public class Customer : Entity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Order> Orders { get; set; } = [];

}
