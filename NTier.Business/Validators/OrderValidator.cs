using FluentValidation;
using NTier.Entities.Models;

namespace NTier.Business.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.CreatedDate)
            .NotEmpty().WithMessage("Sipariş tarihi boş geçilemez.");
            
            RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Müşteri alanı boş geçilemez.");

            RuleFor(x => x.TotalAmount)
            .GreaterThanOrEqualTo(1).WithMessage("Toplam tutar 0'dan büyük olmalıdır.");
        }
    }
}
