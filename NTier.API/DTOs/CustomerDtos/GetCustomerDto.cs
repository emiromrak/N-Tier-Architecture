using NTier.Entities.Models;

namespace NTier.API.DTOs
{
    public class GetCustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
