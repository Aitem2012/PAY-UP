using FluentValidation;
using PAY_UP.Application.Dtos.Authentication;

namespace PAY_UP.Application.Validators.Authentication
{
    public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
    {
        public ConfirmEmailDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Token).NotEmpty().WithMessage("{propertyName} cannot be null or empty");
        }
    }
}
