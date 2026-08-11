namespace NTier.API.DTOs
{
    public class UpdateProductDto
    {
        public required string Name { get; set; }
        public double UnitPrice { get; set; }
        public int UnitInStock { get; set; }
        public bool Discontinued { get; set; }
        public Guid? CategoryID { get; set; }
    }
}
