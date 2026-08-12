using NTier.Entities.Abstractions;

namespace NTier.Entities.Models;

public class Order : Entity
{
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }

        // Navigation property
        public ICollection<Product> Products { get; set; } = [];
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
}