using FluentValidation;
using PAY_UP.Application.Dtos.Common;

namespace PAY_UP.Application.Validators{
    public class PaymentDtoValidator : AbstractValidator<PaymentDto>{
        public PaymentDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("{propertyName} is required");
            RuleFor(x => x.Amount).GreaterThan(0).NotEmpty().NotNull().WithMessage("{propertyName} is required");
        }
    }
}