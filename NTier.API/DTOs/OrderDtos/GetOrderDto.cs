namespace NTier.API.DTOs
{
    public class GetOrderDto
    {
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<Guid> ProductIds { get; set; } = [];
    }
}