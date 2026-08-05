namespace NTier.API.DTOs
{
    public class GetProductDto
    {
        public string? Name { get; set; }
        public double UnitPrice { get; set; }
        public int UnitInStock { get; set; }
    }
}
