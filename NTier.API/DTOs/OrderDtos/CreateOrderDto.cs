using NTier.Entities.Models;

namespace NTier.API.DTOs
{
    public class CreateOrderDto
    {
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<Guid> ProductIds { get; set; } = [];
    }
}
