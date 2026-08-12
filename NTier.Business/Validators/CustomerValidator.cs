using FluentValidation;
using NTier.Entities.Models;

namespace NTier.Business.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Müşteri adı boş geçilemez.")
            .MinimumLength(3).WithMessage("Müşteri adı minimum 3 karakter olmalıdır.");
        }
    }
}
