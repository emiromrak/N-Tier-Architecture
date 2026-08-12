namespace NTier.API.DTOs
{
    public class GetOrderProductDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public double UnitPrice { get; set; }
        public int UnitInStock { get; set; }
    }
}