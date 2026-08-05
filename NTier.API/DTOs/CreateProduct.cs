namespace NTier.API.DTOs
{
    public class CreateProductDto
    {
        public string? Name { get; set; }
        public double UnitPrice { get; set; }
        public int UnitInStock { get; set; }
    }
}
