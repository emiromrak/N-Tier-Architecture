using FluentValidation;
using NTier.Entities.Models;

namespace NTier.Business.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün alanı boş geçilemez.")
                .MinimumLength(1).WithMessage("Ürün adı minimum 1 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Ürün adı maximum 50 karakter olmalıdır.");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Ürün fiyatı negatif olamaz.");

            RuleFor(x => x.UnitInStock)
                .GreaterThanOrEqualTo(0).WithMessage("Ürün stoku negatif olamaz.");
        }
    }
}
