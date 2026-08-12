namespace NTier.API.DTOs
{
    public class GetOrderDto
    {
        public Guid ID { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public ICollection<GetOrderProductDto> Products { get; set; } = [];
    }
}