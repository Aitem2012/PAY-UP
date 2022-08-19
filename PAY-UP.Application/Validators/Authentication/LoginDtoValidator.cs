using FluentValidation;
using PAY_UP.Application.Dtos.Authentication;

namespace PAY_UP.Application.Validators.Authentication
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty().WithMessage("{propertyName} cannot be null or empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("{propertyName} cannot be null or empty");
        }
    }
}
