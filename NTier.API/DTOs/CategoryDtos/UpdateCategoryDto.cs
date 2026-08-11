namespace NTier.API.DTOs
{
    public class UpdateCategoryDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
