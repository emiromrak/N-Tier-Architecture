using NTier.Entities.Models;

namespace NTier.API.DTOs
{
    public class UpdateCustomerDto
    {
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; }

    }
}
