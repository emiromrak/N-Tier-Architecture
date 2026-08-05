namespace NTier.API.DTOs
{
    public class GetProductsWCategoryDto
    {
        public string? Name { get; set; }
        public double UnitPrice { get; set; }
        public int UnitInStock { get; set; }
        public string? CategoryName { get; set; }

    }
}
