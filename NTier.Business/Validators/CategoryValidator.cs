using FluentValidation;
using NTier.Entities.Models;

namespace NTier.Business.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori alanı boş geçilemez.")
                .MinimumLength(5).WithMessage("Kategori adı minimum 5 karakter olmalıdır.")
                .MaximumLength(25).WithMessage("Kategori adı maximum 25 karakter olmalıdır.")
                .Matches(ReadyRegexes.NoNumberFormat).WithMessage("Lütfen sadece harf girişi yapınız.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama alanı boş geçilemez.")
                .MinimumLength(5).WithMessage("Kategori açıklaması minimum 5 karakter olmalıdır.")
                .MaximumLength(255).WithMessage("Kategori açıklaması 255 karakteri geçmemelidir.");
        }
    }
}
