using FluentValidation;
using PAY_UP.Application.Dtos.Authentication;

namespace PAY_UP.Application.Validators.Authentication
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("{propertyName} cannot be null or empty");
        }
    }
}
