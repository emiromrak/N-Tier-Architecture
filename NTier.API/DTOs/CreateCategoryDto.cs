namespace NTier.API.DTOs
{
    public class CreateCategoryDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
