namespace NTier.API.DTOs
{
    public class GetProductsWCategoryDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public double UnitPrice { get; set; }
        public int UnitInStock { get; set; }
        public bool Discontinued { get; set; }
        public bool IsActive { get; set; }
        public Guid? CategoryID { get; set; }
        public string? CategoryName { get; set; }

    }
}
